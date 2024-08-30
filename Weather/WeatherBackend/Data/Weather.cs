using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Weather.Enum;

namespace Weather.Data
{
    [Table("Weather")]
    public class Weather
    {
        [Key]
        [Column("Id")]
        [Required]
        public Guid Id { get; set; }
        
        [Column("Temperature")]
        [Required]
        public decimal Temperature { get; set; }
        
        [Column("Humidity")]
        [Required]
        public decimal Humidity { get; set; }
        
        [Column("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt")]
        [Required]

        public DateTime UpdatedAt { get; set; }
        
        [Column("IsActive")]
        [Required]
        public IsActive IsActive { get; set; }

    }
}
