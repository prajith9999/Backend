using System.ComponentModel.DataAnnotations;

namespace vueproject_asp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }  // First name property

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }   // Last name property

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }        // Assuming a role property
    }
}
