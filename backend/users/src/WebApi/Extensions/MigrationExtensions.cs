using Infrastructure.Authentication;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace WebApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        DatabaseSettings databaseSettings =
            scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

        EnsureDatabaseExists(databaseSettings);

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

    private static void EnsureDatabaseExists(DatabaseSettings databaseSettings)
    {
        var adminConnection = new NpgsqlConnectionStringBuilder
        {
            Host = databaseSettings.Host,
            Port = databaseSettings.Port,
            Database = "postgres",
            Username = databaseSettings.User,
            Password = databaseSettings.Password
        }.ToString();

        using var connection = new NpgsqlConnection(adminConnection);
        connection.Open();

        using var checkCommand = new NpgsqlCommand(
            "SELECT 1 FROM pg_database WHERE datname = @name;",
            connection);
        checkCommand.Parameters.AddWithValue("name", databaseSettings.Name);

        object? exists = checkCommand.ExecuteScalar();
        if (exists is not null)
        {
            return;
        }

        using var createCommand = new NpgsqlCommand(
            $"CREATE DATABASE \"{databaseSettings.Name}\";",
            connection);
        createCommand.ExecuteNonQuery();
    }
}
