using LabPreTest.Shared.Entities;
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
            await CheckPatientsAsync();
            await CheckMediciansAsync();

        }
        private async Task CheckPatientsAsync()
        {
            if(!_context.Patients.Any())
            {
                _context.Patients.Add(new Patient 
                {
                    Address = "Testing Address",
                    BirthDay = "02/02/1997",
                    Cellphone = 31245711,
                    Name = "NameTest",
                    Email = "Testing@gmail.com",
                    UserName = "FirstUser",
                    DocumentId = "215154141"

                });
            }
            await _context.SaveChangesAsync();   
        }
        private async Task CheckMediciansAsync()
        {
            if (!_context.Medicians.Any())
            {
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address",
                    BirthDay = "02/02/1997",
                    Cellphone = 31245711,
                    Name = "NameTest",
                    Email = "Testing@gmail.com",
                    UserName = "FirstUser",
                    DocumentId = "215154141"

                });
            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new() { Name = "Medellín" },
                                new() { Name = "Itagüí" },
                                new() { Name = "Envigado" },
                                new() { Name = "Bello" },
                                new() { Name = "Rionegro" },
                                new() { Name = "Marinilla" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new() { Name = "Usaquen" },
                                new() { Name = "Champinero" },
                                new() { Name = "Santa fe" },
                                new() { Name = "Useme" },
                                new() { Name = "Bosa" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Orlando" },
                                new() { Name = "Miami" },
                                new() { Name = "Tampa" },
                                new() { Name = "Fort Lauderdale" },
                                new() { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
