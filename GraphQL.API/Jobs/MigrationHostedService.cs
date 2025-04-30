using GraphQL.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Jobs
{
    public class MigrationHostedService<TContext>(IServiceProvider serviceProvider, Func<TContext, IServiceProvider, Task> seeder) : IHostedService where TContext : DbContext
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return serviceProvider.MigrateDbContextAsync(seeder);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
