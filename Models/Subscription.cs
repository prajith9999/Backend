using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class Subscription
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string SubscriptionName { get; set; }

        [Required]
        public decimal Features { get; set; }

        public int? Price { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
