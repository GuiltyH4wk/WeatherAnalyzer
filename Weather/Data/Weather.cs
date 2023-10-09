using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Data
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

    }
}
