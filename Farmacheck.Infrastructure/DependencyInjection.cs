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

        services.AddHttpClient<IChecklistApiClient, ChecklistApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ChecklistApi:BaseUrl"]!);
        });

        services.AddHttpClient<IResponseFormatCatApiClient, ResponseFormatCatApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ResponseFormatCatApi:BaseUrl"]!);
        });

        services.AddHttpClient<IChecklistSectionApiClient, ChecklistSectionApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ChecklistSectionApi:BaseUrl"]!);
        });

        services.AddHttpClient<IGroupingTagApiClient, GroupingTagApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["GroupingTagApi:BaseUrl"]!);
        });

        services.AddHttpClient<IQuestionApiClient, QuestionApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["QuestionApi:BaseUrl"]!);
        });

        services.AddHttpClient<IZoneApiClient, ZonesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ZonesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IRoleApiClient, RolesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["RolesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IMenuApiClient, MenusApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["MenusApi:BaseUrl"]!);
        });

        services.AddHttpClient<IPermissionApiClient, PermissionsApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["PermissionsApi:BaseUrl"]!);
        });

        services.AddHttpClient<IPermissionByRoleApiClient, PermissionByRolesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["PermissionByRolesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IBusinessStructureApiClient, BusinessStructureApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BusinessStructureApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CategoriesApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICategoryByQuestionnaireApiClient, CategoryByQuestionnaireApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CategoriesByQuestionnairesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IPeriodicityByQuestionnaireApiClient, PeriodicityByQuestionnaireApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["PeriodicityByQuestionnaireApi:BaseUrl"]!);
        });

        services.AddHttpClient<ITaskPriorityApiClient, TaskPriorityApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["TaskPriorityApi:BaseUrl"]!);
        });

        services.AddHttpClient<ITaskOriginApiClient, TaskOriginApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["TaskOriginApi:BaseUrl"]!);
        });

        services.AddHttpClient<ISprintApiClient, SprintApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["SprintApi:BaseUrl"]!);
        });

        services.AddHttpClient<ITaskApiClient, TaskApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["TaskApi:BaseUrl"]!);
        });

        services.AddHttpClient<IHierarchyByRoleApiClient, HierarchyByRolesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["HierarchyByRolesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IUserApiClient, UsersApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["UsersApi:BaseUrl"]!);
        });

        services.AddHttpClient<IUserByRoleApiClient, UsersByRolesApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["UsersByRolesApi:BaseUrl"]!);
        });

        services.AddHttpClient<IClientesAsignadosArolPorUsuariosApiClient, ClientesAsignadosArolPorUsuariosApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ClientesAsignadosArolPorUsuariosApi:BaseUrl"]!);
        });

        services.AddHttpClient<ICustomersRolesUsersApiClient, CustomersRolesUsersApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["CustomersRolesUsersApi:BaseUrl"]!);
        });

        services.AddHttpClient<IQuizAssignmentManagerApiClient, QuizAssignmentManagerApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["QuizAssignmentManagerApi:BaseUrl"]!);
        });

        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["AuthApi:BaseUrl"]!);
        });

        services.AddHttpClient<IMailingProgramacionClient, MailingProgramacionClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["MailingProgramacionApi:BaseUrl"]!);
        });

        services.AddHttpClient<INotificationCenterApiClient, NotificationCenterSettingApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["NotificationCenterApi:BaseUrl"]!);
        });

        return services;
    }
}
