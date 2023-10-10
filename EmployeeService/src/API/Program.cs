using EmployeeService.Application.Employees.Queries;
using EmployeeService.Domain.Abstractions;
using EmployeeService.Infastructure;
using EmployeeService.Infastructure.Caching;
using EmployeeService.Infastructure.Persistent;
using MediatR;
using Serilog;

namespace API;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(typeof(GetEmployeesQueryHandler).Assembly);

        builder.Services.AddSingleton(builder.Configuration.GetRequiredSection(nameof(RepositoryOptions)).Get<RepositoryOptions>());
        builder.Services.AddSingleton(builder.Configuration.GetRequiredSection(nameof(CacheOptions)).Get<CacheOptions>());

        builder.Services.AddTransient<IEmployeeRepository, JsonEmployeeRepository>();
        builder.Services.AddTransient<ICacheService, CacheService>();

        builder.Services.AddMemoryCache();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {NewLine}{Exception}")
            .WriteTo.File(
                            path: "logs/.log",
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {NewLine}{Exception}",
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: 1024 * 1024 * 10)
            .CreateLogger();

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });
        builder.Services.AddCors(p => p.AddPolicy("employeeAngular", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("employeeAngular");
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}