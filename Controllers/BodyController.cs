
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace vueproject_asp.Models
{
    public class Bodyy
    {
        // The 'ID' property is mapped to 'id' in JSON for consistency
        [Key]
        [JsonPropertyName("id")]
        public int ID { get; set; }

        // The internal 'Id' property is mapped to 'id' in JSON to avoid conflicts
        [JsonPropertyName("id")]
        public int Id { get; internal set; }

        // 'Title' property is mapped to 'title' in JSON
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        // 'TitleDescription' property is mapped to 'title_description' in JSON
        [MaxLength(256)]
        [JsonPropertyName("title_description")]
        public string TitleDescription { get; set; }

        // 'OrderNumber' property is mapped to 'order_number' in JSON
        [Required]
        [JsonPropertyName("order_number")]
        public int OrderNumber { get; set; }

        // 'CreatedBy' property is mapped to 'created_by' in JSON
        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }

        // 'CreatedDate' property is mapped to 'created_date' in JSON
        [JsonPropertyName("created_date")]
        public DateTime? CreatedDate { get; set; }

        // 'ModifiedBy' property is mapped to 'modified_by' in JSON
        [JsonPropertyName("modified_by")]
        public int? ModifiedBy { get; set; }

        // 'ModifiedDate' property is mapped to 'modified_date' in JSON
        [JsonPropertyName("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        // 'DeletedBy' property is mapped to 'deleted_by' in JSON
        [JsonPropertyName("deleted_by")]
        public int? DeletedBy { get; set; }

        // 'DeletedDate' property is mapped to 'deleted_date' in JSON
        [JsonPropertyName("deleted_date")]
        public DateTime? DeletedDate { get; set; }
    }
}
