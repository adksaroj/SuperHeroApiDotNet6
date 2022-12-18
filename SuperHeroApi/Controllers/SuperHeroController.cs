using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest($"Hero with id {id} not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            return await _context.SuperHeros.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var supHero = await _context.SuperHeros.FindAsync(hero.Id);
            if(supHero == null)
                return BadRequest($"Hero with id {hero.Id} not found.");
            supHero.FirstName = hero.FirstName;
            supHero.LastName =hero.LastName;
            supHero.Place = hero.Place;

            await _context.SaveChangesAsync();

            return await _context.SuperHeros.ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero == null)
                return BadRequest($"Hero with id {id} not found.");
            _context.SuperHeros.Remove(hero);
            await _context.SaveChangesAsync();
            return await _context.SuperHeros.ToListAsync();
        }
    }
}
