using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Farmacheck.Application.Interfaces;
using Farmacheck.Infrastructure.Services;

namespace Farmacheck.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IBrandApiClient, BrandApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BrandApi:BaseUrl"]!);
        });

        services.AddHttpClient<IBusinessUnitApiClient, BusinessUnitApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BusinessUnitApi:BaseUrl"]!);
        });

        services.AddHttpClient<ISubbrandApiClient, SubbrandApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["SubbrandApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICustomersApiClient, CustomersApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CustomersApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICustomerTypesApiClient, CustomerTypesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CustomerTypesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IZoneApiClient, ZonesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ZonesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IBusinessStructureApiClient, BusinessStructureApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BusinessStructureApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICategoryByQuestionnaireApiClient, CategoryByQuestionnaireApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CategoriesByQuestionnairesApi:BaseUrl"]!);
        });

        return services;
    }
}
