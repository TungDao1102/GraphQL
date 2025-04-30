namespace GraphQL.API.Models
{
    public class BrandSubscription
    {
        public int BrandId { get; set; }

        public Brand? Brand { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
