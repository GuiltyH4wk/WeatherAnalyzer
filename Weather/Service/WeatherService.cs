using Weather.Data.DataBaseContext;

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



        }

    }
}
