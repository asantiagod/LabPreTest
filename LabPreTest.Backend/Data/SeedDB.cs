using Microsoft.EntityFrameworkCore.Internal;
using System.Security.AccessControl;

namespace LabPreTest.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //update database por codigo
            await CheckCountriesAsync();

        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any()) 
            {
                _context.Countries.Add(new Shared.Entities.Country { Name = "Colombia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Canada" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Usa" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Filipinas" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Italia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Alemania" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Francia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Escocia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Irlanda" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Inglaterra" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "El salvador"});
                _context.Countries.Add(new Shared.Entities.Country { Name = "Noruega" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
