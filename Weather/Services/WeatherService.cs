using HeroesDatabase.DataBaseContext;
using HeroesDatabase.Model;
using HeroesDatabase.Services.InterfaceService;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace HeroesDatabase.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HeroesDatabaseContext _context;
        public WeatherService (HeroesDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Weather>> GetWeather()
        {
            if (_context.Weather == null)
            {
                throw new Exception();
            }

            List<Data.Weather> Weatheres = await _context.Weather.ToListAsync();
            List<Weather> WeatheresDTO = new List<Weather>();

            foreach (Data.Weather Weather in Weatheres)
            {
                Weather WeatherDTO = new Weather();
                this.CreateDto(WeatherDTO,Weather);
                WeatheresDTO.Add(WeatherDTO);

            }
            return WeatheresDTO;
        }
        public async Task<Weather> GetWeather(Guid WeatherId)
        {
            if (_context.Weather == null)
            {
                throw new Exception();

            }
            Data.Weather Weather = await _context.Weather.FindAsync(WeatherId);

            if (Weather == null)
            {
                throw new Exception();
            }

            Weather WeatherDTO = new Weather();
            this.CreateDto(WeatherDTO, Weather);

            return WeatherDTO;
        }

        public async Task PutWeather(Guid WeatherId,WeatherPersist WeatherPersist)
        {
            Data.Weather Weather = await _context.Weather.FindAsync(WeatherId);

            Weather.Name = WeatherPersist.Name;
            Weather.UpdateAt = DateTime.UtcNow;


            _context.Weather.Update(Weather);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(WeatherId))
                {
                    throw new Exception();
                }
                else
                {
                    throw;
                }
            }
            return;
        }


        public async Task<Weather> PostWeather(WeatherPersist WeatherPersist)
        {

            if (_context.Weather == null)
            {
                throw new Exception();
            }


            Data.Weather Weather = new Data.Weather();
            Weather.Id = Guid.NewGuid();
            Weather.Name = WeatherPersist.Name;
            Weather.CompanyId = WeatherPersist.CompanyId;
            Weather.CreatedAt = DateTime.UtcNow;
            Weather.UpdateAt = DateTime.UtcNow;

            _context.Weather.Add(Weather);
            await _context.SaveChangesAsync();

            Weather WeatherDTO = new Weather();

            this.CreateDto(WeatherDTO, Weather);

            return WeatherDTO;
        }

        public async Task DeleteWeather(Guid WeatherId)
        {
            if (_context.Weather == null)
            {
                throw new Exception();
            }
            var Weather = await _context.Weather.FindAsync(WeatherId);
            if (Weather == null)
            {
                throw new Exception();
            }

            _context.Weather.Remove(Weather);
            await _context.SaveChangesAsync();

            return;
        }



        Weather CreateDto(Weather WeatherDTO,Data.Weather Weather) 
        {
            WeatherDTO.Id = Weather.Id;
            WeatherDTO.Name = Weather.Name;
            WeatherDTO.CompanyId = Weather.CompanyId;
            WeatherDTO.CreatedAt = Weather.CreatedAt;
            WeatherDTO.UpdateAt = Weather.UpdateAt;
            return WeatherDTO;
        }


        private bool WeatherExists(Guid id)
        {
            return (_context.Weather?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
