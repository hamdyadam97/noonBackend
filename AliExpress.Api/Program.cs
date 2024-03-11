
using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Application.Mapper;
using AliExpress.Application.Services;
using AliExpress.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using None.Infrastructure;

namespace AliExpress.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AliExpressContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AliExpressContext>();
            //mapper
            builder.Services.AddAutoMapper(M =>M.AddProfile(new MappingProduct()));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingCategory)));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingSubCategory)));

            //Repository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();


            //service
            builder.Services.AddScoped<ICategoryService ,CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
