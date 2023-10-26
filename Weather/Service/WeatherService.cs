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

        public async Task GetWeather(List<Guid> id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            List<Model.Weather> model = new List<Model.Weather>();

            model = await _context.Weather.Where(x => x.Id.Equals(id)).ToListAsync();

        }

        public async Task UpdateWeather(Model.WeatherPersist data)
        {
            if(data == null) throw new ArgumentNullException(nameof(data));

            bool isUpdate = false;
            if(data.Id != Guid.Empty) isUpdate = true;
            
            Data.Weather model = new Data.Weather();

            if(isUpdate)
            {
                model.Temperature = data.Temperature;
                model.Humidity = data.Humidity;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Update(model);
            }
            else
            { 
                model.Id = Guid.NewGuid();
                model.Temperature = data.Temperature;
                model.Humidity = data.Humidity;
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;
                model.IsActive = IsActive.Active;

                _context.Add(model);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWeather(Model.Weather data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            Data.Weather model = new Data.Weather();

            data.IsActive = IsActive.inActive;

            _context.Update(model);
            await _context.SaveChangesAsync();
        }

    }
}
