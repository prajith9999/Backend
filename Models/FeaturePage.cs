using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class FeaturePage
    {
        internal readonly object Description;

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [MaxLength(128)]
        public string HeadingDescription { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
