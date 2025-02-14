using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc; // For IActionResult

namespace LandWind.Models
{
    public class Footer
    {
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonPropertyName("footerTitle")]  // This corresponds to FooterTitle in the database
        public string FooterTitle { get; set; }

        [JsonPropertyName("footerDescription")]  // Added for FooterDescription
        public string FooterDescription { get; set; }

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
