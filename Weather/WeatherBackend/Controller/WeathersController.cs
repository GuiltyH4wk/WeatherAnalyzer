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
    public class WeathersController : Controller
    {
        private readonly WeatherContext _context;
        private readonly IWeatherService _weatherService;

        public WeathersController(WeatherContext context, IWeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Weathers
        public async Task<Model.Weather> Index(Guid weatherId)
        {
            return await _weatherService.GetWeatherById(weatherId);
        }

        // GET: Weathers/Details/5
        public async Task<Model.Weather> Details(Guid? id)
        {
            if (id == null || _context.Weather == null) throw new ArgumentNullException(nameof(id));

            Model.Weather data = await _weatherService.GetWeatherById(id.Value);

            if (data == null) throw new ArgumentNullException(nameof(id));

            return await _weatherService.GetWeatherById(id.Value);
        }

        // GET: Weathers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weathers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[Route("create")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Temperature,Humidity,CreateAt")] Model.Weather weather)
        {
            if (ModelState.IsValid)
            {
                weather.Id = Guid.NewGuid();
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weather);
        }

        // GET: Weathers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Weather == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[Route("update")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Temperature,Humidity,CreateAt")] Model.Weather weather)
        {
            if (id != weather.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherExists(weather.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weather);
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
        [HttpPost, ActionName("Delete")]
		[Route("delete")]
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
