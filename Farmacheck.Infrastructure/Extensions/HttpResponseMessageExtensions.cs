using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Farmacheck.Infrastructure.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessWithDetailsAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync();
            var message = BuildErrorMessage(response, content);

            throw new HttpRequestException(message);
        }

        private static string BuildErrorMessage(HttpResponseMessage response, string content)
        {
            var statusMessage = $"Request failed with status {(int)response.StatusCode} ({response.ReasonPhrase}).";
            var details = ExtractErrorDetails(content);

            if (string.IsNullOrWhiteSpace(details))
            {
                return statusMessage;
            }

            return $"{statusMessage} Details: {details}";
        }

        private static string ExtractErrorDetails(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return string.Empty;
            }

            try
            {
                using var document = JsonDocument.Parse(content);
                var root = document.RootElement;

                if (root.TryGetProperty("errors", out var errorsElement))
                {
                    var messages = new List<string>();
                    CollectMessages(errorsElement, messages);
                    if (messages.Count > 0)
                    {
                        return string.Join(" | ", messages);
                    }
                }

                if (root.TryGetProperty("message", out var messageProp) && messageProp.ValueKind == JsonValueKind.String)
                {
                    return messageProp.GetString() ?? string.Empty;
                }

                if (root.TryGetProperty("error", out var errorProp) && errorProp.ValueKind == JsonValueKind.String)
                {
                    return errorProp.GetString() ?? string.Empty;
                }

                if (root.TryGetProperty("title", out var titleProp) && titleProp.ValueKind == JsonValueKind.String)
                {
                    return titleProp.GetString() ?? string.Empty;
                }
            }
            catch (JsonException)
            {
                // If the content is not valid JSON we just fall back to returning the raw text.
            }

            return content;
        }

        private static void CollectMessages(JsonElement element, List<string> messages)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    var value = element.GetString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        messages.Add(value);
                    }
                    break;
                case JsonValueKind.Array:
                    foreach (var item in element.EnumerateArray())
                    {
                        CollectMessages(item, messages);
                    }
                    break;
                case JsonValueKind.Object:
                    foreach (var property in element.EnumerateObject())
                    {
                        CollectMessages(property.Value, messages);
                    }
                    break;
            }
        }
    }
}
