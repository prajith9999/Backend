using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LandWind.Models
{
    public class Body
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

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
