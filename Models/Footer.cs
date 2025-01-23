using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class Footer
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }  // The primary key mapped as 'id' in JSON

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("page_id")]
        public string PageId { get; set; }

        [MaxLength(512)]
        [JsonPropertyName("footer_title")]
        public string FooterTitle { get; set; }

        [JsonPropertyName("footer_description")]
        public string FooterDescription { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

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
        public Footer() { }

        // Constructor to ensure required properties are initialized
        public Footer(string pageId)
        {
            PageId = pageId ?? throw new ArgumentNullException(nameof(pageId));
        }
    }
}
