
using Application.Abstractions.Data;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {



        services.AddOptions<DatabaseSettings>()
            .Bind(configuration.GetSection("Database"))
            .Validate(ValidateDatabaseSettings);


        services
            .AddDatabase(configuration)
            .AddServices();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddDbContext<ApplicationDbContext>(
            (serviceProvider,options) => {

                DatabaseSettings? databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

                if (databaseSettings is null)
                {
                    throw new InvalidOperationException("Database settings are not configured properly.");
                }

                string host = databaseSettings.Host;
                int port = databaseSettings.Port;
                string databaseName = databaseSettings.Name;
                string username = databaseSettings.User;
                string password = databaseSettings.Password;


                var connectionString = new NpgsqlConnectionStringBuilder
                {
                    Host = host,
                    Port = port,
                    Database = databaseName,
                    Username = username,
                    Password = password

                }.ToString();

                options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Default))
                .UseSnakeCaseNamingConvention();

                
                });


        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());



        return services;
    }



     private static bool ValidateDatabaseSettings(DatabaseSettings options)
    {
        return string.IsNullOrEmpty(options.Host) == false
            && options.Port > 0
            && string.IsNullOrEmpty(options.Name) == false
            && string.IsNullOrEmpty(options.User) == false
            && string.IsNullOrEmpty(options.Password) == false;
    }

}