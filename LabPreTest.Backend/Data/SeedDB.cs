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
            await CheckMediciansAsync();
            await CheckPatientsAsync();
        }
        private async Task CheckMediciansAsync()
        {
            Random rnd = new Random(1000000000);
            if (!_context.Medicians.Any())
            {
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address",
                    BirthDay = "02/02/1997",
                    Cellphone = rnd.Next(),
                    Name = "NameTest",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser1",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address2",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest2",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser2",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address3",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest3",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser3",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address4",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest4",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser4",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address5",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest5",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser5",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address6",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest6",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser6",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address7",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest7",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser7",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Medicians.Add(new Medician
                {
                    Address = "Testing Address8",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest8",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser8",
                    DocumentId = $"{rnd.Next()}"
                });

            }
            await _context.SaveChangesAsync();
        }
        private async Task CheckPatientsAsync()
        {
            Random rnd = new Random(1000000000);
            if (!_context.Patients.Any())
            {
                _context.Patients.Add(new Patient 
                {
                    Address = "Testing Address",
                    BirthDay = "02/02/1997",
                    Cellphone = rnd.Next(),
                    Name = "NameTest",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser1",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address2",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest2",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser2",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address3",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest3",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser3",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address4",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest4",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser4",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address5",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest5",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser5",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address6",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest6",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser6",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address7",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest7",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser7",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address8",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next(),
                    Name = "NameTest8",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser8",
                    DocumentId = $"{rnd.Next()}"
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
