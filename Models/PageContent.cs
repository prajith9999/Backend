using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class PageContent
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [MaxLength(128)]
        public string HighLights { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string Content { get; set; } // Ensure Content is defined as a string type
    }
}
