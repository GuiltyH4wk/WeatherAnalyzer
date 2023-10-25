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
        public Guid Id { get; set; }
        [Column("temperature")]
        public float Temperature { get; set; }
        [Column("humidity")]
        public float Humidity { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [Column("isActive")]
        public IsActive IsActive { get; set; }

    }
}
