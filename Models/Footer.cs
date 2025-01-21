using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class Footer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string PageId { get; set; }

        [MaxLength(512)]
        public string FooterTitle { get; set; }

        public string FooterDescription { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string Content { get; set; }
    }
}
