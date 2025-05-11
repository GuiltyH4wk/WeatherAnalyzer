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

            bool isUpdate = this.IsValidId(data.Id);
            
            Data.Weather model = new Data.Weather();

            if(!isUpdate)
            { 
                model.Id = Guid.NewGuid();
                model.CreatedAt = DateTime.UtcNow;
                model.IsActive = IsActive.Active;
            }

			model.Temperature = data.Temperature;
			model.Humidity = data.Humidity;
			model.UpdatedAt = DateTime.UtcNow;


			if (isUpdate) _context.Update(model);
            else _context.Add(model);


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

		//public async void Mars(Guid id)
		//{

		//	List<Data.Weather> model = new List<Data.Weather>();
		//	Model.Weather data = new Model.Weather();

		//	model = await _context.Weather.Where(x => x.CreatedAt.Day == DateTime.Now.Day && x.IsActive == IsActive.Active).ToListAsync();

  //          double[][] xTraining = new double[][]			
  //          {
		//		new double[] { 30.0, 70.0 }, // 30°C, 70% humidity
  //              new double[] { 25.0, 60.0 }, // 25°C, 60% humidity
  //              new double[] { 20.0, 80.0 }, // 20°C, 80% humidity
  //              new double[] { 35.0, 50.0 }, // 35°C, 50% humidity
  //              new double[] { 15.0, 90.0 }, // 15°C, 90% humidity
  //              new double[] { 28.0, 65.0 }, // 28°C, 65% humidity
  //              new double[] { 22.0, 75.0 }, // 22°C, 75% humidity
  //              new double[] { 18.0, 85.0 }, // 18°C, 85% humidity
  //          };




		//	if (model == null) throw new ArgumentNullException(nameof(model));

		//	data.Temperature = model.Temperature;
		//	data.Humidity = model.Humidity;
		//	data.CreatedAt = model.CreatedAt;

		//	return data;
		//}

	}
}
