using dotnet_webapi_demo_01_christenzarif.DTO;
using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Implement;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using System.Text;

namespace dotnet_webapi_demo_01_christenzarif
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var corses = "_myAllowSpecificOrigins";
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            // Custom Services
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dotnet_webapi_demo_01_christenzarif")));
            builder.Services.AddScoped<IEmployee, EmployeeRepository>();
            builder.Services.AddScoped<IDepartment, DepartmentRepository>();
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

            //[Authorize] check JWT Token
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:signin_key"]!))
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corses,
                                  policy =>
                                  {
                                      policy.WithOrigins(builder.Configuration.GetSection("Cors").Get<string[]>()!);
                                  });
            });

            builder.Services.Configure<FormOptions>(opt =>
            {
                opt.MultipartBodyLengthLimit = 50 * 1024 * 1024;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // wwwroot

            app.UseCors(corses);

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
