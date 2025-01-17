using System;
using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }  // Corrected: Ensure this property exists

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
