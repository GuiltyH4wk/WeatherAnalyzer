using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Weather.Enum;

namespace Weather.Data
{
    [Table("Weather")]
    public class Weather
    {
        [Key]
        [Column("id")]
        [Required]
        public Guid Id { get; set; }
        
        [Column("temperature")]
        [Required]
        public float Temperature { get; set; }
        
        [Column("humidity")]
        [Required]
        public float Humidity { get; set; }
        
        [Column("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        [Required]

        public DateTime UpdatedAt { get; set; }
        
        [Column("isActive")]
        [Required]
        public IsActive IsActive { get; set; }

    }
}
