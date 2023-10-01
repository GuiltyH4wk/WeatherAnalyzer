using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroesDatabase.Data;
using System.Collections.Immutable;
using HeroesDatabase.DataBaseContext;

namespace HeroesDatabase.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroesDatabaseContext _context;

        public HeroesController(HeroesDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroesDto>>> GetHeroes()
        {

            List<Heroes> heroes = await _context.Heroes.ToListAsync();
            List<HeroesDto> heroesDtos = new List<HeroesDto>();
            foreach (Heroes hero in heroes)
            {
                HeroesDto heroesDto = new HeroesDto();
                heroesDto.Id = hero.Id;
                heroesDto.Name = hero.Name;
                heroesDtos.Add(heroesDto);
            }
            return heroesDtos;
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroesDto>> GetHeroes(int id)
        {
            Heroes heroes = await _context.Heroes.FindAsync(id);


            if (heroes == null)
            {
                return NotFound();
            }
            HeroesDto heroesDto = new HeroesDto();
            heroesDto.Id = heroes.Id;
            heroesDto.Name = heroes.Name;

            return heroesDto;
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroes(int id, HeroesDto heroesDto)
        {
            if (id != heroesDto.Id)
            {
                return BadRequest();
            }

            Heroes heroes = await _context.Heroes.FindAsync(id);

            heroes.Id = heroesDto.Id;
            heroes.Name = heroesDto.Name;

            _context.Heroes.Update(heroes);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            return NoContent();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeroesDto>> PostHeroes(HeroesDto heroesDto)
        {
            Heroes heroes = new Heroes();
            heroes.Id = heroesDto.Id;
            heroes.Name = heroesDto.Name;


            _context.Heroes.Add(heroes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHeroes", new { id = heroesDto.Id }, heroesDto);
            // _context.Heroes.Add(heroes);
        }


        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroes(int id)
        {
            var heroes = await _context.Heroes.FindAsync(id);
            if (heroes == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(heroes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroesExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
