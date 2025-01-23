using System;

namespace vueproject_asp.Models
{
    public class Subscription
    {
        public int ID { get; set; }  // Unique identifier for the subscription
        public string Name { get; set; }  // Name of the subscription plan (e.g., "Basic", "Premium")
        public DateTime StartDate { get; set; }  // The start date of the subscription
        public DateTime EndDate { get; set; }    // The end date of the subscription
        public decimal Price { get; set; }       // The price of the subscription
    }
}
