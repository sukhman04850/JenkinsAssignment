
using Products_Microservice.BusinessLayer.Mapper;
using Products_Microservice.BusinessLayer;
using Products_Microservice.Interfaces;
using Products_Microservice.Repository;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace Products_Microservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductRepositoryBL, ProductRepositoryBL>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<IHealthCheckHandler, ScopedEurekaHealthCheckHandler>();
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            builder.Services.AddDiscoveryClient();
            builder.Services.AddHealthChecks();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.MapControllers();
            app.UseHealthChecks("/info");
            app.Run();
        }
    }
}
