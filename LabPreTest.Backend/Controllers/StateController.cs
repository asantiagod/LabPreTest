using LabPreTest.Backend.Data;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.States.AsNoTracking().ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var states = await _context.States.FindAsync(id);
            if (states == null)
            {
                return NotFound();
            }
            return Ok(states);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(State states)
        {
            _context.Add(states);
            await _context.SaveChangesAsync();
            return Ok(states);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(State states)
        {
            _context.Update(states);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var states = await _context.States.FindAsync(id);
            if (states == null)
            {
                return NotFound();
            }
            _context.Remove(states);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
