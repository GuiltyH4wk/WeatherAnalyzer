using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Weather.Enum;

namespace Model
{
    [Table("Weather")]
    public class Weather
    {

        public Guid Id { get; set; }

        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime UpdatedAt { get; set; }

        public IsActive IsActive { get; set; }
    }


    public class WeatherPersist
    {
        public Guid Id { get; set; }

        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PersonValidator : AbstractValidator<Weather>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Temperature).NotNull();
            RuleFor(x => x.Humidity).NotNull();
            RuleFor(x => x.CreatedAt).NotNull();
            RuleFor(x => x.UpdatedAt).NotNull();
            RuleFor(x => x.IsActive).NotNull();
        }
    }

}
