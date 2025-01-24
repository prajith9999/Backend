using System;
using Microsoft.AspNetCore.Mvc; // For IActionResult
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LandWind.Models; // This should be included if models are in this namespace


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
