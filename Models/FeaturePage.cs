using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LandWind.Models
{
    public class FeaturePage
    {
        [Key]
        [JsonPropertyName("PageId")]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("FooterTitle")]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }  // Make sure this property is here

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
