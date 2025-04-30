namespace GraphQL.API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<BrandSubscription> Subscriptions { get; set; } = [];
    }
}
