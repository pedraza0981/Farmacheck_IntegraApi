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
            builder.Services.AddAutoMapper(typeof(WebMappingProfile), typeof(BrandProfile));

            builder.Services.AddHttpClient<IBrandApiClient, BrandApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["BrandApi:BaseUrl"]!);
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
