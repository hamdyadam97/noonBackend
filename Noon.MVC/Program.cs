using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using None.Infrastructure;
using System.Globalization;

namespace Noon.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Dependency Injection 
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();


            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();



            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSession();


            //Dependency Injection Context
            builder.Services.AddDbContext<AliExpressContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceContex"));
            }, ServiceLifetime.Scoped);

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<AliExpressContext>();
            builder.Services.AddSession();


            //        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            //        builder.Services.AddControllersWithViews()
            //            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            //        builder.Services.Configure<RequestLocalizationOptions>(options =>
            //        {
            //            var supportedCultures = new[]
            //            {
            //    new CultureInfo("en-US"),
            //    new CultureInfo("ar-EG"),
            //};
            //            options.DefaultRequestCulture = new RequestCulture("en-US");
            //            options.SupportedUICultures = supportedCultures;
            //        });

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddControllersWithViews()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
             {
                 var supportedCultures = new[]
                 {
              new CultureInfo("en-US"),
              new CultureInfo("ar-EG"), // Arabic (Egypt)
           };
                 options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en-US", uiCulture: "en-US");
                 options.SupportedCultures = supportedCultures;
                 options.SupportedUICultures = supportedCultures;
                 options.RequestCultureProviders = new List<IRequestCultureProvider>
             {         new CookieRequestCultureProvider(),       // Accept culture from cookies
                      new QueryStringRequestCultureProvider(),  // Accept culture from query string
                      new AcceptLanguageHeaderRequestCultureProvider() // Accept culture from header    };
             };
             });
            
            var app = builder.Build();

            app.UseSession();
            app.UseRequestLocalization();
            app.MapRazorPages();
            ////var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            ////app.UseRequestLocalization(options.Value);
            app.UseAuthentication();
            app.UseWebSockets();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
