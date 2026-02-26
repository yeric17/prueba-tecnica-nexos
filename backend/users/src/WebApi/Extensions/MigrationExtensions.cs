using Infrastructure.Authentication;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace WebApi.Extensions;

public static class MigrationExtensions
{
    private const int MaxRetries = 10;
    private const int DelayMilliseconds = 2000;

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        DatabaseSettings databaseSettings =
            scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

        EnsureDatabaseExists(databaseSettings);

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        ExecuteWithRetry(() => dbContext.Database.Migrate(), "Applying migrations");
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

        ExecuteWithRetry(() =>
        {
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
        }, "Ensuring database exists");
    }

    private static void ExecuteWithRetry(Action action, string operationName)
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            try
            {
                action();
                Console.WriteLine($"{operationName} succeeded on attempt {attempt}");
                return;
            }
            catch (Exception ex) when (attempt < MaxRetries)
            {
                Console.WriteLine($"{operationName} failed on attempt {attempt}/{MaxRetries}. Error: {ex.Message}");
                Console.WriteLine($"Retrying in {DelayMilliseconds}ms...");
                Thread.Sleep(DelayMilliseconds);
            }
        }

        // If all retries failed, execute one more time to throw the exception
        action();
    }
}
