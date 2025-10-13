using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotesApp.DAL.Options;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Utils;

namespace NotesApp.DAL;

/// <summary>
/// Класс-расширение для подключения сервисов базы данных в DI
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    /// Добавить сервисы базы данных в DI
    /// </summary>
    /// <param name="services">Коллекция сервисов DI</param>
    /// <param name="configuration">Конфигурация приложения</param>
    public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        var seedUser = configuration.GetSection(SeedUserOption.OptionName).Get<User>();

        seedUser.PasswordHash = StringHasher.ToHash(seedUser.PasswordHash);
        seedUser.CreatedAtUtc = new DateTime(2025, 11, 11).ToUniversalTime();
        seedUser.UpdatedAtUtc = new DateTime(2025, 11, 11).ToUniversalTime();

        var connStr = configuration.GetConnectionString("DockerDefault");
        services.AddDbContext<NotesAppDbContext>(opt =>
        {
            opt.UseNpgsql(connStr, dbOpt =>
            {
                dbOpt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
            });

            opt.UseSeeding((dbContext, _) =>
            {
                var dbSet = dbContext.Set<User>();
                if (!dbSet.Any(u => u.Id == seedUser.Id))
                {
                    dbContext.Set<User>().Add(seedUser);
                    dbContext.SaveChanges();
                }
            });

            opt.UseAsyncSeeding(async (dbContext, _, cancellationToken) =>
            {
                var dbSet = dbContext.Set<User>();
                if (!await dbSet.AnyAsync(u => u.Id == seedUser.Id, cancellationToken))
                {
                    await dbContext.Set<User>().AddAsync(seedUser, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            });

            opt.ConfigureWarnings(builder =>
            {
                builder.Ignore(RelationalEventId.PendingModelChangesWarning);
            });
        });

        services.AddRepositories();
    }

    /// <summary>
    /// Применить миграции к базе данных
    /// </summary>
    /// <param name="services">Провайдер сервисов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public static async Task EnsureDatabaseMigratedAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var logger = scope.ServiceProvider.GetService<ILogger<NotesAppDbContext>>();

        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<NotesAppDbContext>();
            var pending = (await dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();

            if (pending.Any())
            {
                await dbContext.Database.MigrateAsync(cancellationToken);

                logger.LogInformation("Applied {Count} database migrations: {Migrations}", pending.Count(), string.Join(", ", pending));
            }
            else
            {
                logger.LogInformation("No pending migrations to apply");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to apply database migrations");
            throw;
        }
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        //TODO: Add Repositories
    }
}