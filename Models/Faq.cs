using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class Faq
    {
        public string? Question { get;  set; }
        public string? Answer { get;  set; }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public int? OrderNumber { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
