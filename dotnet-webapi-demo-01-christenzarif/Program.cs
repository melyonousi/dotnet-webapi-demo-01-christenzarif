using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Implement;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace dotnet_webapi_demo_01_christenzarif
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Custom Services
            // database connection
            var dbHost = Environment.GetEnvironmentVariable("DB_SERVER");
            var dbName = Environment.GetEnvironmentVariable("DB_DATABASE");
            var dbUserId = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            //var connectionString = $"Server=sql.bsite.net\\MSSQL2016;Database=casetrue_;Persist Security Info=False;User ID=casetrue_;Password=FMyQPzl06jgSrS5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;Connection Timeout=30;";
            var connectionString = builder.Configuration?.GetConnectionString("dotnet_webapi_demo_01_christenzarif")?
                .Replace("{DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER"))
                .Replace("{DB_DATABASE}", Environment.GetEnvironmentVariable("DB_DATABASE"))
                .Replace("{DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
                .Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IEmployee, EmployeeRepository>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://casetrue.runasp.net",
                                                          "http://casetrue.bsite.net",
                                                          "https://casetrue.bsite.net",
                                                          "https://casetrue.azurewebsites.net",
                                                          "http://casetrue.azurewebsites.net",
                                                          "https://dotnet-webapi-demo-01-christenzarif.onrender.com",
                                                          "http://dotnet-webapi-demo-01-christenzarif.onrender.com");
                                  });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
