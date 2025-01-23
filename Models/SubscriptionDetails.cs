using System;

namespace vueproject_asp.Models
{
    public class SubscriptionDetails
    {
        internal readonly object ID;

        public int DetailID { get; set; }  // Primary Key
        public int SubscriptionID { get; set; }  // Foreign Key for Subscription
        public string FeatureDescription { get; set; }  // Description of the feature
        public string DetailType { get; set; }  // Type of the detail (e.g., Plan, Addon)
        public DateTime CreatedDate { get; set; }  // Date when the record was created
        public DateTime? ModifiedDate { get; set; }  // Date when the record was last modified
        public string DeletedBy { get; set; }  // User who deleted the record (if applicable)
    }
}
