using AliExpress.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Noon.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();





            //Dependency Injection Context
            builder.Services.AddDbContext<AliExpressContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceContex"));
            }, ServiceLifetime.Scoped);

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<AliExpressContext>();




            var app = builder.Build();






           







            app.MapRazorPages(); ///
            app.UseAuthentication();



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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
