using LabPreTest.Shared.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace LabPreTest.Backend.Data
{
    internal class SeedDBCountryHelper
    {
        public static async Task SetCountriesSeed(DataContext context)
        {
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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
            context.Countries.Add(new Country
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

            await context.SaveChangesAsync();
        }
    }
}