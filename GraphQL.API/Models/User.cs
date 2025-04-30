namespace GraphQL.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ReceivedArrivalNotificationId { get; set; }
        public bool? IsSupportAgent { get; set; }
        public ICollection<BrandSubscription> Subscriptions { get; set; } = [];
    }
}
