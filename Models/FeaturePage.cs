using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class FeaturePage
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }  // The primary key mapped as 'id' in JSON

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("heading_description")]
        public string HeadingDescription { get; set; }

        [Required]
        [JsonPropertyName("order_number")]
        public int OrderNumber { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        // These fields should be managed by the server, not the client
        [JsonIgnore]
        public int? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore]
        public int? ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public int? DeletedBy { get; set; }

        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        // Parameterless constructor for Dapper
        public FeaturePage() { }

        // Constructor to ensure required properties are initialized
        public FeaturePage(string title, string headingDescription)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            HeadingDescription = headingDescription ?? throw new ArgumentNullException(nameof(headingDescription));
        }
    }
}
