using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc; // For IActionResult
using LandWind.Models; 


namespace LandWind.Models
{
    public class Faq
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
