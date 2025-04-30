using GraphQL.API.Data;
using GraphQL.API.Jobs;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Extensions
{
    public static class MigrateDbContextExtension
    {
        private static IServiceCollection AddMigration<TContext>(this IServiceCollection services, Func<TContext, IServiceProvider, Task> seeder) where TContext : DbContext
        {
            services.AddHostedService(sp => new MigrationHostedService<TContext>(sp, seeder));
            return services;
        }

        public static IServiceCollection AddMigration<TContext, TDbSeeder>(this IServiceCollection services) where TContext : DbContext where TDbSeeder : class, IDbSeeder<TContext>
        {
            services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
            return services.AddMigration<TContext>((context, sp)
               => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
        }

        public static async Task MigrateDbContextAsync<TContext>(this IServiceProvider services, Func<TContext, IServiceProvider, Task> seeder) where TContext : DbContext
        {
            using var scope = services.CreateScope();
            var scopeServices = scope.ServiceProvider;
            var logger = scopeServices.GetRequiredService<ILogger<TContext>>();
            var context = scopeServices.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation(
                    "Migrating database associated with context {DbContextName}",
                    typeof(TContext).Name);

                // auto retry if ExecuteAsync false in some case of error
                var strategy = context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(() => InvokeSeeder(seeder, context, scopeServices));
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while migrating the database used on context {DbContextName}",
                    typeof(TContext).Name);
                throw;
            }
        }

        private static async Task InvokeSeeder<TContext>(Func<TContext, IServiceProvider, Task> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            await context.Database.MigrateAsync();
            await seeder(context, services);
        }
    }
}
