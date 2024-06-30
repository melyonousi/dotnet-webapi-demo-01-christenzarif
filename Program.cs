
using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Implement;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace dotnet_webapi_demo_01_christenzarif
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Custom Services
            // database connection
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUserId = Environment.GetEnvironmentVariable("USER_ID");
            var dbPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
            var connectionString = $"Server=tcp:casetrue.database.windows.net,1433;Database=casetrue;Persist Security Info=False;User ID=casetrue;Password=kiY8v6IVq~4&lG;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
