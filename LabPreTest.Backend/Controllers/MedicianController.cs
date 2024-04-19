using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabPreTest.Backend.Data;
using LabPreTest.Shared.Entities;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MediciansController : ControllerBase
    {
        private readonly DataContext _context;

        public MediciansController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Medicians.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var medician = await _context.Medicians.FindAsync(id);
            if (medician == null)
            {
                return NotFound();
            }
            return Ok(medician);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Medician medician)
        {
            _context.Add(medician);
            await _context.SaveChangesAsync();
            return Ok(medician);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Medician medician)
        {
            _context.Update(medician);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var medician = await _context.Medicians.FindAsync(id);
            if (medician == null)
            {
                return NotFound();
            }
            _context.Remove(medician);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
