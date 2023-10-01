using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroesDatabase.Data
{
    [Table("Weather")]
    public class Weather
    {
        [Key]
        [Column('id')]
        public Guid Id { get; set; }

        [Column('name')]
        public String Name { get; set; }

        [Column('createdAt')]
        public DateTime CreatedAt { get; set; }

        [Column('updateAt')]
        public DateTime UpdateAt { get; set; }
    }

}
