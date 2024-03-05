using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model;
using Weather.Data.DataBaseContext;
using Weather.Service.Interface;

namespace Weather
{
	[Route("weather")]
	[ApiController]
	public class WeathersController : Controller
    {
        private readonly WeatherContext _context;
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeathersController> _logger;



		public WeathersController(WeatherContext context, IWeatherService weatherService, ILogger<WeathersController> logger)
		{
			this._logger = logger;
			this._context = context;
			this._weatherService = weatherService;
		}

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("update")]
        public async Task Persist([FromBody] WeatherPersist weather)
        {
			this._logger.LogDebug("update weather: " + weather);

            await this._weatherService.UpdateWeather(weather);

        }

        // GET: Weathers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Weather == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpDelete("delete")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Weather == null)
            {
                return Problem("Entity set 'WeatherContext.Weather'  is null.");
            }
            var weather = await _context.Weather.FindAsync(id);
            if (weather != null)
            {
                _context.Weather.Remove(weather);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherExists(Guid id)
        {
            return (_context.Weather?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
