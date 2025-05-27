using Assignment.Interfaces;
using Assignment.MapperConfigs;
using Assignment.Models;
using Assignment.Repositories;
using Assignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("MonsterDb")));
            builder.Services.AddScoped<IContentDataRepository, ContentDataRepository>();
            builder.Services.AddScoped<IContentDataService, ContentDataService>();
            builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
            builder.Services.AddAutoMapper(typeof(DefaultProfile));
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Data}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
