using CustomerApi.Repositories;
using CustomerApi.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Customerapi
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

            builder.Services.AddDbContext<CustomersContext>(mysqlBuilder =>
            {
                mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
            });

            builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();

            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .WithOrigins("http://127.0.0.1:5500")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}