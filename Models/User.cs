using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class User
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }  // Unique identifier for the user

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("username")]
        public string Username { get; set; }  // Username for login

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        [JsonPropertyName("email")]
        public string Email { get; set; }  // User's email address

        [Required]
        [MinLength(8)]
        [JsonPropertyName("password")]
        public string Password { get; set; }  // Password for the user

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }  // Full name of the user

        [Phone]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }  // User's phone number

        // These fields should be managed by the server, not the client
        [JsonIgnore]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public string ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public string DeletedBy { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        // Constructor to ensure required properties are initialized
        public User() { }

        // Constructor to initialize a user with necessary fields
        public User(string username, string email, string password)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
