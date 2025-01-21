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

        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("created_date")]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("modified_by")]
        public int? ModifiedBy { get; set; }

        [JsonPropertyName("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("deleted_by")]
        public int? DeletedBy { get; set; }

        [JsonPropertyName("deleted_date")]
        public DateTime? DeletedDate { get; set; }
    }
}
