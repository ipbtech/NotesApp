﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Utils;

namespace NotesApp.DAL
{
    public static class ServiceExtension
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var seedUser = configuration.GetSection(SeedUser.OptionName).Get<User>();
            seedUser.PasswordHash = StringHasher.ToHash(seedUser.PasswordHash);
            seedUser.CreatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime();
            seedUser.UpdatedAtUtc = new DateTime(2025, 1, 1).ToUniversalTime();

            var connStr = configuration.GetConnectionString("DockerDefault");
            services.AddDbContext<NotesAppDbContext>(opt =>
            {
                opt.UseNpgsql(connStr);
                opt.UseSeeding((dbContext, _) =>
                {
                    var dbSet = dbContext.Set<User>();
                    if (!dbSet.Any(u => u.Id == seedUser.Id))
                    {
                        dbContext.Set<User>().Add(seedUser);
                        dbContext.SaveChanges();
                    }
                });
                opt.ConfigureWarnings(builder =>
                {
                    builder.Ignore(RelationalEventId.PendingModelChangesWarning);
                });
            });
        }
    }
}