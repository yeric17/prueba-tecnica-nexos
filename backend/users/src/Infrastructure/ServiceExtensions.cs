using Application.Abstractions.Authentication;
using Domain.Users;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;

namespace Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("Jwt"))
            .Validate(ValidateJwtSettings);

        services.AddOptions<DatabaseSettings>()
            .Bind(configuration.GetSection("Database"))
            .Validate(ValidateDatabaseSettings);


        services
            .AddDatabase(configuration)
            .AddAuthentication(configuration);
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
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

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


        JwtSettings jwtSettings = new JwtSettings
        {
            Secret = configuration["Jwt:Secret"]!,
            ExpirationInMinutes = int.Parse(configuration["Jwt:ExpirationInMinutes"]!),
            Issuer = configuration["Jwt:Issuer"]!,
            Audience = configuration["Jwt:Audience"]!
        };

        if(ValidateJwtSettings(jwtSettings) == false)
        {
            throw new InvalidOperationException("JWT settings are not configured properly.");
        }

        string secret = jwtSettings.Secret;
        string issuer = jwtSettings.Issuer;
        string audience = jwtSettings.Audience;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        services.AddHttpContextAccessor();

        return services;

    }

    private static bool ValidateJwtSettings(JwtSettings options)
    {
        return string.IsNullOrEmpty(options.Secret) == false
            && options.ExpirationInMinutes > 0
            && string.IsNullOrEmpty(options.Issuer) == false
            && string.IsNullOrEmpty(options.Audience) == false;
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