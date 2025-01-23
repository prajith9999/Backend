using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class Faq
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }  // The primary key mapped as 'id' in JSON

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("question")]
        public string? Question { get; set; }

        [JsonPropertyName("answer")]
        public string? Answer { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("order_number")]
        public int? OrderNumber { get; set; }

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
        public Faq() { }

        // Constructor to ensure required properties are initialized
        public Faq(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}
