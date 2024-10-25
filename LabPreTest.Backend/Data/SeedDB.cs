using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Migrations;
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
            await CheckUserAsync("222222",
                    "Second",
                    "User",
                    "v",
                    "2222222222",
                    "second street of second city",
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
                        Status = OrderStatus.Idle
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
                Conditions = new List<TestCondition>()

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
                if (condition != null)
                    test.Conditions.Add(new TestCondition { Condition = pCondition });
            }

            _context.Tests.Add(test);
        }

        private async Task CheckMediciansAsync()
        {
            Random rnd = new Random(1000000000);
            if (!_context.Medicians.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.Medicians.Add(new Medic
                    {
                        Address = $"Testing Address {i}",
                        BirthDay = DateTime.ParseExact("02/02/1997", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        Cellphone = rnd.Next().ToString(),
                        Name = $"MedicianName {i}",
                        Email = $"{rnd.Next()}@gmail.com",
                        UserName = $"FirstUser{i}",
                        DocumentId = $"{rnd.Next()}"
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckPatientsAsync()
        {
            Random rnd = new Random(1000000000);
            if (!_context.Patients.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.Patients.Add(new Patient
                    {
                        Address = $"Testing Address {i}",
                        BirthDay = DateTime.ParseExact("02/02/1997", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        Cellphone = rnd.Next().ToString(),
                        Name = $"PatientName {i}",
                        Email = $"{rnd.Next()}@yopmail.com",
                        UserName = $"PatientUserName{i}",
                        DocumentId = $"{rnd.Next()}"
                    });
                }
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
                                new() { Name = "Abejorral" },
                                new() { Name = "Abriaquí" },
                                new() { Name = "Alejandría" },
                                new() { Name = "Amagá" },
                                new() { Name = "Amalfi" },
                                new() { Name = "Andes" },
                                new() { Name = "Barbosa" },
                                new() { Name = "Betania" },
                                new() { Name = "Betulia" },
                                new() { Name = "Briceño" },
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
                        new State()
                        {
                            Name = "Caldas",
                            Cities = [
                                new(){Name = "Aguadas"},
                                new(){Name = "Anserma"},
                                new(){Name = "Aranzazu"},
                                new(){Name = "Belalcázar"},
                                new(){Name = "Chinchiná"},
                                new(){Name = "Filadelfia"},
                                new(){Name = "La Dorada"},
                                new(){Name = "La Merced"},
                                new(){Name = "Manizales"},
                                new(){Name = "Manzanares"},
                            ]
                        },
                        new State()
                        {
                            Name = "Caquetá",
                            Cities = [
                                new() { Name = "Albania"},
                                new() { Name = "Belén de los Andaquíes"},
                                new() { Name = "Cartagena de Chairá"},
                                new() { Name = "Curillo"},
                                new() { Name = "El Doncello"},
                                new() { Name = "El Paujil"},
                                new() { Name = "Florencia"},
                                new() { Name = "La Montañita"},
                                new() { Name = "Milán"},
                                new() { Name = "Puerto Rico"},
                                new() { Name = "San José del Fragua"},
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
                _context.Countries.Add(new Country
                {
                    Name = "Venezuela",
                    States =
                    [
                        new State()
                        {
                            Name = "Maracaibo",
                            Cities = [
                                new() { Name = "Ciudad ven1" },
                                new() { Name = "Ciudad ven2" },
                                new() { Name = "Ciudad ven3" },
                                new() { Name = "Ciudad ven4 " },
                                new() { Name = "Ciudad ven5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Amazonas",
                            Cities = [
                                new() { Name = "Ciudad ven6" },
                                new() { Name = "Ciudad ven7" },
                                new() { Name = "Ciudad ven8" },
                                new() { Name = "Ciudad ven9" },
                                new() { Name = "El Ciudad ven10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States =
            [
                new State()
                        {
                            Name = "Patagonia",
                            Cities = [
                                new() { Name = "Ciudad Argentina 1" },
                                new() { Name = "Ciudad Argentina 2" },
                                new() { Name = "Ciudad Argentina 3" },
                                new() { Name = "Ciudad Argentina 4" },
                                new() { Name = "Ciudad Argentina 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Tierra del fuego",
                            Cities = [
                                new() { Name = "Ciudad Argentina 6" },
                                new() { Name = "Ciudad Argentina 7" },
                                new() { Name = "Ciudad Argentina 8" },
                                new() { Name = "Ciudad Argentina 9" },
                                new() { Name = "Ciudad Argentina 10" },
                            ]
                        },
                        new State()
                        {
                            Name = "Chaco",
                            Cities = [
                                new() { Name = "Aviá Terai"},
                                new() { Name = "Barranqueras"},
                                new() { Name = "Basail"},
                                new() { Name = "Colonia Benítez"},
                                new() { Name = "Concepción del Bermejo"},
                                new() { Name = "Corzuela"},
                                new() { Name = "Departamento del Maipú"},
                                new() { Name = "Departamento de Libertad"},
                                new() { Name = "Fontana"},
                                new() { Name = "Gancedo"},
                                new() { Name = "Hermoso Campo"},
                                new() { Name = "La Tigra"},
                                new() { Name = "Lapachito"},
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "España",
                    States =
            [
                new State()
                        {
                            Name = "Valencia",
                            Cities = [
                                new() { Name = "Ciudad española 1" },
                                new() { Name = "Ciudad española 2" },
                                new() { Name = "Ciudad española 3" },
                                new() { Name = "Ciudad española 4" },
                                new() { Name = "Ciudad española 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Galicia",
                            Cities = [
                                new() { Name = "Ciudad española 6" },
                                new() { Name = "Ciudad española 7" },
                                new() { Name = "Ciudad española 8" },
                                new() { Name = "Ciudad española 9" },
                                new() { Name = "Ciudad española 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Alemania",
                    States =
            [
                new State()
                        {
                            Name = "Baviera",
                            Cities = [
                                new() { Name = "Ciudad alemana 1" },
                                new() { Name = "Ciudad alemana 2" },
                                new() { Name = "Ciudad alemana 3" },
                                new() { Name = "Ciudad alemana 4" },
                                new() { Name = "Ciudad alemana 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Sajonia",
                            Cities = [
                                new() { Name = "Ciudad alemana 6" },
                                new() { Name = "Ciudad alemana 7" },
                                new() { Name = "Ciudad alemana 8" },
                                new() { Name = "Ciudad alemana 9" },
                                new() { Name = "Ciudad alemana 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Rusia",
                    States =
            [
                new State()
                        {
                            Name = "Yakutia",
                            Cities = [
                                new() { Name = "Ciudad rusa 1" },
                                new() { Name = "Ciudad rusa 2" },
                                new() { Name = "Ciudad rusa 3" },
                                new() { Name = "Ciudad rusa 4" },
                                new() { Name = "Ciudad rusa 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Kamchatka",
                            Cities = [
                                new() { Name = "Ciudad rusa 6" },
                                new() { Name = "Ciudad rusa 7" },
                                new() { Name = "Ciudad rusa 8" },
                                new() { Name = "Ciudad rusa 9" },
                                new() { Name = "Ciudad rusa 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Australia",
                    States =
            [
                new State()
                        {
                            Name = "Queensland",
                            Cities = [
                                new() { Name = "Ciudad Australiana 1" },
                                new() { Name = "Ciudad Australiana 2" },
                                new() { Name = "Ciudad Australiana 3" },
                                new() { Name = "Ciudad Australiana 4" },
                                new() { Name = "Ciudad Australiana 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "New South Wales",
                            Cities = [
                                new() { Name = "Ciudad Australiana 6" },
                                new() { Name = "Ciudad Australiana 7" },
                                new() { Name = "Ciudad Australiana 8" },
                                new() { Name = "Ciudad Australiana 9" },
                                new() { Name = "Ciudad Australiana 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Mexico",
                    States =
            [
                new State()
                        {
                            Name = "Chihuahua",
                            Cities = [
                                new() { Name = "Ciudad mexicana 1" },
                                new() { Name = "Ciudad mexicana 2" },
                                new() { Name = "Ciudad mexicana 3" },
                                new() { Name = "Ciudad mexicana 4" },
                                new() { Name = "Ciudad mexicana 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Nuevo León",
                            Cities = [
                                new() { Name = "Ciudad mexicana 6"},
                                new() { Name = "Ciudad mexicana 7" },
                                new() { Name = "Ciudad mexicana 8" },
                                new() { Name = "Ciudad mexicana 9" },
                                new() { Name = "Ciudad mexicana 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Francia",
                    States =
                 [
                     new State()
                        {
                            Name = "Normandia",
                            Cities = [
                                new() { Name = "Ciudad francesa 1"},
                                new() { Name = "Ciudad francesa 2" },
                                new() { Name = "Ciudad francesa 3" },
                                new() { Name = "Ciudad francesa 4" },
                                new() { Name = "Ciudad francesa 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Betrana",
                            Cities = [
                                new() { Name = "Ciudad francesa 6"},
                                new() { Name = "Ciudad francesa 7" },
                                new() { Name = "Ciudad francesa 8" },
                                new() { Name = "Ciudad francesa 9" },
                                new() { Name = "Ciudad francesa 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Italia",
                    States =
                 [
                     new State()
                        {
                            Name = "Piemonte",
                            Cities = [
                                new() { Name = "Ciudad italiana 1"},
                                new() { Name = "Ciudad italiana 2" },
                                new() { Name = "Ciudad italiana 3" },
                                new() { Name = "Ciudad italiana 4" },
                                new() { Name = "Ciudad italiana 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Toscana",
                            Cities = [
                                new() { Name = "Ciudad italiana 6"},
                                new() { Name = "Ciudad italiana 7" },
                                new() { Name = "Ciudad italiana 8" },
                                new() { Name = "Ciudad italiana 9" },
                                new() { Name = "Ciudad italiana 10" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Ecuador",
                    States =
                    [
                new State()
                        {
                            Name = "Esmeralda",
                            Cities = [
                                new() { Name = "Ciudad ecuatoriana 1" },
                                new() { Name = "Ciudad ecuatoriana 2" },
                                new() { Name = "Ciudad ecuatoriana 3" },
                                new() { Name = "Ciudad ecuatoriana 4" },
                                new() { Name = "Ciudad ecuatoriana 5" },
                            ]
                        },
                        new State()
                        {
                            Name = "Pichincha",
                            Cities = [
                                new() { Name = "Ciudad ecuatoriana 6" },
                                new() { Name = "Ciudad ecuatoriana 7" },
                                new() { Name = "Ciudad ecuatoriana 8" },
                                new() { Name = "Ciudad ecuatoriana 9" },
                                new() { Name = "Ciudad ecuatoriana 10" },
                            ]
                        },
                    ]
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}