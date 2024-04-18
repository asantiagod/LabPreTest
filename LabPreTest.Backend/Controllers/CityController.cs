using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabPreTest.Backend.Data;
using LabPreTest.Shared.Entities;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Cities.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            return Ok(cities);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(City cities)
        {
            _context.Add(cities);
            await _context.SaveChangesAsync();
            return Ok(cities);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(City cities)
        {
            _context.Update(cities);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            _context.Remove(cities);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
