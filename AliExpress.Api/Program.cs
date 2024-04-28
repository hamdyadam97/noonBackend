
using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Application.Mapper;
using AliExpress.Application.Services;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using None.Infrastructure;
using System.Text;

namespace AliExpress.Api
{  public class Program
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
            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<AliExpressContext>().AddDefaultTokenProviders();

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddRoles<IdentityRole>().AddEntityFrameworkStores<AliExpressContext>().AddDefaultTokenProviders();







            //mapper
            builder.Services.AddAutoMapper(M =>M.AddProfile(new MappingProduct()));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingCategory)));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingSubCategory)));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingCart)));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingOrder)));
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingPayment)));

            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingUser)));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            //Repository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartItemRepository,CartItemRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            //service
            builder.Services.AddScoped<ICategoryService ,CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartItemService ,CartItemService>();
            builder.Services.AddScoped<ICartService,CartService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentMethodService,PaymentMethodService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout= TimeSpan.FromSeconds(20);
                options.Cookie.HttpOnly= true;
                options.Cookie.IsEssential= true;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            // Add JWT authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            //builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<IProductRepository,ProductRepository>();
            //builder.Services.AddScoped<ICategoryService, CategoryService>();
            //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            //builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseCors("AllowAngularDev");
            app.UseAuthorization();
            
            app.UseSession();

            app.MapControllers();

            app.Run();
        }
    }
}
