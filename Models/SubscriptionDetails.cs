using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vueproject_asp.Models
{
    public class SubscriptionDetails
    {
        [Key]
        public int DetailID { get; set; }

        [ForeignKey("Subscription")]
        public int SubscriptionID { get; set; }

        [Required]
        [MaxLength(512)]
        public string FeatureDescription { get; set; }

        [Required]
        [MaxLength(64)]
        public string? DetailType { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}
