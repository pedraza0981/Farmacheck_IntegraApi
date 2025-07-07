using Farmacheck.Infrastructure.Services;
using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Application.Mappings;
using Farmacheck.Helpers;
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
            builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(BrandProfile), typeof(BusinessUnitProfile), typeof(SubbrandProfile), typeof(CustomerProfile));

            builder.Services.AddHttpClient<IBrandApiClient, BrandApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["BrandApi:BaseUrl"]!);
            });

            builder.Services.AddHttpClient<IBusinessUnitApiClient, BusinessUnitApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["BusinessUnitApi:BaseUrl"]!);
            });

            builder.Services.AddHttpClient<ISubbrandApiClient, SubbrandApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["SubbrandApi:BaseUrl"]!);
            });

            builder.Services.AddHttpClient<ICustomersApiClient, CustomersApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["CustomersApi:BaseUrl"]!);
            });

            builder.Services.AddHttpClient<IZoneApiClient, ZonesApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ZonesApi:BaseUrl"]!);
            });

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
