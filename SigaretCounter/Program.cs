using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SigaretCounter.Models;
using System.Configuration;

namespace SigaretCounter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<DbConnectionInfo>(settings => builder.Configuration.GetSection("ConnectionStrings").Bind(settings));
            builder.Services.AddScoped<XgbRackotpgContext>();

            //builder.Services.AddDbContext<XgbRackotpgContext>(options =>
            //options.UseNpgsql(builder.Configuration.GetConnectionString("XgbRackotpgContext")));

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}