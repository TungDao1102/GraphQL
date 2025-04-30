using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Data
{
    public interface IDbSeeder<in TContext> where TContext : DbContext
    {
        Task SeedAsync(TContext context);
    }
}
