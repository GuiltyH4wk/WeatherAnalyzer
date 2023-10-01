using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HeroesDatabase.Model
{

    [Table("Weather")]
    public class Weather
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    public class WeatherPersist
    {

        public string Name { get; set; }
    }
}
