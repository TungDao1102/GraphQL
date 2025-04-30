using GraphQL.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQL.API.Data.Configurations
{
    public class BrandSubscriptionConfiguration : IEntityTypeConfiguration<BrandSubscription>
    {
        public void Configure(EntityTypeBuilder<BrandSubscription> builder)
        {
            builder.ToTable("BrandSubscriptions")
                    .HasKey(s => new { s.BrandId, s.UserId });

            builder.HasOne(u => u.User);
            builder.HasOne(u => u.Brand);
        }
    }
}
