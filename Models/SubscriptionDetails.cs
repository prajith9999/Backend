namespace vueproject_asp.Models
{
    public class SubscriptionDetails
    {
        public int ID { get; set; }
        public int SubscriptionID { get; set; }  // Foreign key for Subscription
        public string Detail { get; set; }
        public string Value { get; set; }
    }
}
