using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.Users
{
    public class UpdateUserPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
