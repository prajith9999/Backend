using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class SocialMedia
    {
        [Key]
        [MaxLength(128)]
        public string ID { get; set; }

        [Required]
        [MaxLength(512)]
        public string PlatformName { get; set; }

        public string IconUrl { get; set; }

        public string ProfileUrl { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
