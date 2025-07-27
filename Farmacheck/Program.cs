using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Mappings;
using Farmacheck.Helpers;
using Farmacheck.Infrastructure;
namespace Farmacheck
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddAutoMapper(typeof(BrandProfile).Assembly);
            //builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(BrandProfile));
            builder.Services.AddAutoMapper(
                typeof(WebMappingProfile),
                typeof(BrandProfile),
                typeof(BusinessUnitProfile),
                typeof(SubbrandProfile),
                typeof(BusinessStructureProfile),
                typeof(CustomerProfile),
                typeof(CustomerTypeProfile),
                typeof(ZoneProfile),
                typeof(CategoryByQuestionnaireProfile),
                typeof(RoleProfile),
                typeof(PermissionProfile),
                typeof(HierarchyByRoleProfile));

            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Security}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
