using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class Body
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }  // The primary key mapped as 'id' in JSON

        [Required]
        [MaxLength(50)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [MaxLength(256)]
        [JsonPropertyName("title_description")]
        public string TitleDescription { get; set; }

        [Required]
        [JsonPropertyName("order_number")]
        public int OrderNumber { get; set; }

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
    }
}
