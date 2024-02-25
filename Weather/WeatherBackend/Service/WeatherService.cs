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

        public async Task<Model.Weather> GetWeatherById(Guid id)
        {

            Data.Weather model = new Data.Weather();
            Model.Weather data = new Model.Weather();


            model = await _context.Weather.Where(x => x.Id.Equals(id) && x.IsActive == IsActive.Active).FirstAsync();

            if (model == null) throw new ArgumentNullException(nameof(model));

            data.Temperature = model.Temperature;
            data.Humidity = model.Humidity;
            data.CreatedAt = model.CreatedAt;

            return data;
        }

        public async Task UpdateWeather(Model.WeatherPersist data)
        {
            if(data == null) throw new ArgumentNullException(nameof(data));

            isUpdate = this.IsValidId(data.Id);
            
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

		public Boolean IsValidId(Guid? id)
		{
			return id.HasValue && id.Value != Guid.Empty;
		}

	}
}
