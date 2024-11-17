using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.InteropServices;

namespace LabPreTest.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDB(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //update database por codigo
            await CheckCountriesAsync();
            await CheckMediciansAsync();
            await CheckPatientsAsync();
            await CheckPreanalyticConditionAsync();
            await CheckSectionAsync();
            await CheckTestTubeAsync();
            await CheckTestAsync();
            await CheckPreanalyticConditionAsync();
            //await CheckOrdersAsync();

            await CheckRolesAsync();
            await CheckUserAsync("123456789",
                                 "Default",
                                 "User",
                                 "default.user@yopmail.com",
                                 "3140000123",
                                 "any street in any city",
                                 UserType.Admin);
            await CheckUserAsync("111111",
                    "First",
                    "User",
                    "first.user@yopmail.com",
                    "1111111111",
                    "first street of first city",
                    UserType.User);
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document,
                                                string firstName,
                                                string lastName,
                                                string email,
                                                string phone,
                                                string address,
                                                UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckOrdersAsync()
        {
            if (!_context.Orders.Any())
            {
                var patients = _context.Patients;
                var medicians = _context.Medicians;

                for (int i = 0; i <= 13; i++)
                {
                    _context.Orders.Add(new Order
                    {
                        CreatedAt = DateTime.Now,
                        Status = OrderStatus.OrdenCreada
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckPreanalyticConditionAsync()
        {
            if (!_context.PreanalyticConditions.Any())
            {
                _context.PreanalyticConditions.Add(new PreanalyticCondition
                {
                    Name = "Ayuno",
                    Description = "No ingerir alimentos durante 8 horas.",
                });

                _context.PreanalyticConditions.Add(new PreanalyticCondition
                {
                    Name = "Nada",
                    Description = "No existe ninguna restricción.",
                });

                _context.PreanalyticConditions.Add(new PreanalyticCondition
                {
                    Name = "Supresión",
                    Description = "Supresión de medicamentos.",
                });

                _context.PreanalyticConditions.Add(new PreanalyticCondition
                {
                    Name = "Especial",
                    Description = "Necesita supervición especial.",
                });
                _context.PreanalyticConditions.Add(new PreanalyticCondition
                {
                    Name = "Sin licor",
                    Description = "Sin consumo de licor en las últimas 24 horas",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTestTubeAsync()
        {
            if (!_context.TestTubes.Any())
            {
                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Dorada",
                    Description = "Activador de coagulación y gel para la separación de suero."
                });

                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Azul",
                    Description = "Con citrato de sodio"
                });
                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Roja",
                    Description = "Recubierta de silicona."
                });

                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Naranja",
                    Description = "Activador de coagulación a base de trombina."
                });

                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Verde",
                    Description = "Heparína de sodio."
                });

                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Blanca",
                    Description = "K2EDTA y gel para la separación de plasma."
                });
                _context.TestTubes.Add(new TestTube
                {
                    Name = "Tapa Lila",
                    Description = "Tupo con EDTA dipotásico."
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckSectionAsync()
        {
            if (!_context.Section.Any())
            {
                await AddSectionAsync("Hematología", new List<string>() { "hematology.png" });
                await AddSectionAsync("Inmunología", new List<string>() { "inmunology.png" });
                await AddSectionAsync("Química General", new List<string>() { "clinicalchemistry.png" });
                await AddSectionAsync("Hemostasia", new List<string>() { "hemostasy.png" });
                await AddSectionAsync("Microbiología", new List<string>() { "microbiology.png" });
                await AddSectionAsync("Biología molecular", new List<string>() { "molecular.jpg" });

                //await AddSectionAsync("Hematología1", new List<string>() { "hematology1.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddSectionAsync(string name, List<string> images)
        {
            Section section = new()
            {
                Name = name,
                SectionImages = new List<SectionImage>()
            };

            foreach (string? image in images)
            {
                string filePath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\sections\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/sections/{image}";
                }
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, ".jpg", "sections");
                section.SectionImages.Add(new SectionImage { Image = imagePath });
            }

            _context.Section.Add(section);
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
                await SeedDBCountryHelper.SetCountriesSeed(_context);
        }

        private async Task CheckTestAsync()
        {
            int i = 0;
            if (!_context.Tests.Any())
            {
                await AddTestAsync(++i, "Hemograma", "Hematología", "Tapa lila", ["Nada"]);
                await AddTestAsync(++i, "Vitamina D", "Inmunología", "Tapa Dorada", ["Supresión"]);
                await AddTestAsync(++i, "Perfil lipídico", "Química General", "Tapa Dorada", ["Ayuno", "Sin licor"]);
                await AddTestAsync(++i, "Tiempo de protrombina", "Hemostasia", "Tapa Azul", ["Especial"]);
                await AddTestAsync(++i, "Serología presuntiva para sifílis", "Microbiología", "Tapa Dorada", ["Nada"]);
                await AddTestAsync(++i, "PCR para Covid-19", "Biología molecular", "Tapa Verde", ["Nada"]);

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddTestAsync(int testId,
                                        string name,
                                        string sectionName,
                                        string testTube,
                                        List<string> preanalyticalConditions)
        {
            Test test = new()
            {
                TestID = testId,
                Name = name,
                Conditions = []
            };

            var section = await _context.Section.FirstOrDefaultAsync(s => s.Name == sectionName);
            if (section != null)
                test.Section = section;

            var tube = await _context.TestTubes.FirstOrDefaultAsync(t => t.Name == testTube);
            if (tube != null)
                test.TestTube = tube;

            foreach (var condition in preanalyticalConditions)
            {
                var pCondition = await _context
                                       .PreanalyticConditions
                                       .FirstOrDefaultAsync(c => c.Name == condition);
                if (pCondition != null)
                    test.Conditions.Add(pCondition);
            }

            _context.Tests.Add(test);
        }

        private async Task CheckMediciansAsync()
        {
            if (!_context.Medicians.Any())
            {
                AddMedic("9988776655", "Valentina Salazar", "1994-02-14", "Female", "3112345678", "Calle 100 #20-30, Bogotá", "valentina.s@yopmail.com", "valensal94");
                AddMedic("8877665544", "Fernando Morales", "1980-09-08", "Male", "3123456789", "Carrera 8 #35-50, Cali", "fernando.m@yopmail.com", "fermor80");
                AddMedic("7766554433", "Daniela López", "1996-12-25", "Female", "3134567890", "Avenida 15 #25-80, Medellín", "daniela.l@yopmail.com", "dani.lopez96");
                AddMedic("6655443322", "Jorge Castillo", "1989-03-14", "Male", "3145678901", "Calle 50 #18-22, Cartagena", "jorge.c@yopmail.com", "jorgecas89");
                AddMedic("5544332211", "Isabella Nieto", "1992-07-30", "Female", "3156789012", "Carrera 12 #22-40, Barranquilla", "isabella.n@yopmail.com", "isabellan92");
                AddMedic("4433221100", "Rafael Ortiz", "1987-10-21", "Male", "3167890123", "Calle 44 #10-60, Bogotá", "rafael.o@yopmail.com", "rafaelort87");
                AddMedic("3322110099", "Paola Hernández", "1983-11-05", "Female", "3178901234", "Carrera 6 #18-35, Cali", "paola.h@yopmail.com", "paolah83");
                AddMedic("2211009988", "Andrés Suárez", "1995-04-11", "Male", "3189012345", "Avenida 6 #30-50, Bucaramanga", "andres.s@yopmail.com", "andresua95");
                AddMedic("1100998877", "Carolina Duarte", "1990-01-20", "Female", "3190123456", "Calle 13 #25-20, Manizales", "carolina.d@yopmail.com", "carodu90");
                AddMedic("0099887766", "Camilo Vargas", "1984-05-27", "Male", "3201234567", "Carrera 10 #40-25, Medellín", "camilo.v@yopmail.com", "camiv84");

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPatientsAsync()
        {
            if (!_context.Patients.Any())
            {
                AddPatient("1023456789", "Ana Gómez", "1990-04-15", "Female", "3001234567", "Calle 10 #12-34, Bogotá", "ana.gomez@yopmail.com", "ana_gomez90");
                AddPatient("1122334455", "Juan Pérez", "1985-06-20", "Male", "3012345678", "Carrera 15 #45-67, Cali", "juan.perez@yopmail.com", "juanperez85");
                AddPatient("1234567890", "María Rodríguez", "1993-08-10", "Female", "3023456789", "Avenida 30 #10-20, Medellín", "maria.rod@yopmail.com", "mariarod93");
                AddPatient("2233445566", "Carlos Sánchez", "1978-01-25", "Male", "3034567890", "Calle 25 #5-10, Bogotá", "carlos.s@yopmail.com", "carlos78");
                AddPatient("3344556677", "Laura Ramírez", "1995-09-05", "Female", "3045678901", "Carrera 7 #45-89, Barranquilla", "laura.r@yopmail.com", "laurara95");
                AddPatient("4455667788", "Pedro González", "1992-11-15", "Male", "3056789012", "Avenida 5 #20-15, Cartagena", "pedro.g@yopmail.com", "pedrogonz92");
                AddPatient("5566778899", "Sofía Martínez", "1988-07-22", "Female", "3067890123", "Calle 8 #60-90, Medellín", "sofia.m@yopmail.com", "sofiamart88");
                AddPatient("6677889900", "Diego Castro", "1983-12-02", "Male", "3078901234", "Carrera 12 #30-40, Cali", "diego.c@yopmail.com", "diegocastro83");
                AddPatient("7788990011", "Luisa Fernández", "1997-03-30", "Female", "3089012345", "Calle 50 #10-15, Bucaramanga", "luisa.f@yopmail.com", "luisafern97");
                AddPatient("8899001122", "Andrés Ramírez", "1991-05-18", "Male", "3090123456", "Carrera 9 #55-23, Manizales", "andres.r@yopmail.com", "andresram91");

                await _context.SaveChangesAsync();
            }
        }

        private void AddPatient(string id_document, string name, string birth_date, string gender, string cell_number, string address, string email, string username)
        {
            _context.Patients.Add(new Patient
            {
                DocumentId = id_document,
                Name = name,
                BirthDay = DateTime.ParseExact(birth_date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Gender = string.Compare(gender, "Female", true) == 0 ? GenderType.Female : GenderType.Male,
                Cellphone = cell_number,
                Address = address,
                Email = email,
                UserName = username
            });
        }

        private void AddMedic(string id_document, string name, string birth_date, string gender, string cell_number, string address, string email, string username)
        {
            _context.Medicians.Add(new Medic
            {
                DocumentId = id_document,
                Name = name,
                BirthDay = DateTime.ParseExact(birth_date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Gender = string.Compare(gender, "Female", true) == 0 ? GenderType.Female : GenderType.Male,
                Cellphone = cell_number,
                Address = address,
                Email = email,
                UserName = username
            });
        }
    }
}