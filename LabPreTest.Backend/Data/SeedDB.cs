using LabPreTest.Shared.Entities;

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
            await CheckTestAsync();
            await CheckSectionAsync();
            await CheckTestTubeAsync();
            await CheckPreanalyticConditionAsync();
        }

        private async Task CheckPreanalyticConditionAsync()
        {
            if (!_context.PreanalyticConditions.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.PreanalyticConditions.Add(new PreanalyticCondition
                    {
                        Name = $"ConditionSeed{i}",
                        Description = $"Some description {i}"
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckTestTubeAsync()
        {
            if (!_context.TestTubes.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.TestTubes.Add(new TestTube
                    {
                        Name = $"TestTubeSeed{i}",
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckSectionAsync()
        {
            if (!_context.Section.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.Section.Add(new Section
                    {
                        Name = $"SectionSeed{i}",
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckTestAsync()
        {
            if (!_context.Tests.Any())
            {
                for (int i = 0; i <= 13; i++)
                {
                    _context.Tests.Add(new Test
                    {
                        TestID = i,
                        Name = $"TestSeed{i}",
                        Recipient = $"GenericRecipient{i}",
                        Conditions = $"GenericCondiciotns{i}",
                        Section = $"GenericSection{i}",
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckMediciansAsync()
        {
            Random rnd = new Random(1000000000);
            if (!_context.Medicians.Any())
            {
                for(int i = 0;i <= 13;i++)
                {
                    _context.Medicians.Add(new Medic
                    {
                        Address = $"Testing Address {i}",
                        BirthDay = "02/02/1997",
                        Cellphone = rnd.Next().ToString(),
                        Name = $"NameTest{i}",
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
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address",
                    BirthDay = "02/02/1997",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser1",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address2",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest2",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser2",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address3",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest3",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser3",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address4",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest4",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser4",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address5",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest5",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser5",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address6",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest6",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser6",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address7",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest7",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser7",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address8",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest8",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser8",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address9",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest9",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser9",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address10",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest10",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser10",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address11",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest11",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser11",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address12",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest12",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser12",
                    DocumentId = $"{rnd.Next()}"
                });
                _context.Patients.Add(new Patient
                {
                    Address = "Testing Address13",
                    BirthDay = "02/02/1998",
                    Cellphone = rnd.Next().ToString(),
                    Name = "NameTest13",
                    Email = $"{rnd.Next()}@gmail.com",
                    UserName = "FirstUser13",
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