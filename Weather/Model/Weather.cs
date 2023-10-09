using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Weather")]
    public class Weather
    {

        public Guid Id { get; set; }

        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime CreateAt{ get; set; }
    }


    public class WeatherPersist
    {

        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime CreateAt { get; set; }
    }


}
