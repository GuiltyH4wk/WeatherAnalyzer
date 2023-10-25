using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Weather.Data.DataBaseContext;
using Weather.Enum;

namespace Weather.Service.Interface
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherContext _context;

        public WeatherService(WeatherContext context)
        {
            this._context = context;
        }


        public async Task CreateWeather(Model.WeatherPersist data)
        {
            Data.Weather model = new Data.Weather();

            if(data != null )
            {
                model.Id = new Guid();
                model.Temperature = data.Temperature;
                model.Humidity = data.Humidity;
                model.CreatedAt = DateTime.UtcNow;
                model.IsActive = IsActive.Active;
                
                await _context.SaveChangesAsync();
            }

        }


        public async Task UpdateWeather(Model.Weather data)
        {
            Model.Weather model = new Model.Weather();
            Data.Weather weather = new Data.Weather();

            model = await _context.Weather.Where(x => x.Equals(data.Id) && x.IsActive == IsActive.Active).FirstAsync();

            bool IsUpdated = model == data ? true : false;

            if (IsUpdated)
            {
                weather.Temperature = data.Temperature;
                weather.Humidity = data.Humidity;
                weather.UpdatedAt = DateTime.UtcNow;
            }

        }


        //public async Task DeleteWeather(Model.Weather data)
        //{
        //    Model.Weather model = new Model.Weather();

        //    model = await _context.Weather.Where(x => x.Equals(data.Id) && x.IsActive == IsActive.Active).FirstAsync();

        //    bool IsUpdated = model == data ? true : false;

        //    if (IsUpdated)
        //    {
        //        weather.Temperature = data.Temperature;
        //        weather.Humidity = data.Humidity;
        //        weather.UpdatedAt = DateTime.UtcNow;
        //    }
        //    else

        //}

    }
}
