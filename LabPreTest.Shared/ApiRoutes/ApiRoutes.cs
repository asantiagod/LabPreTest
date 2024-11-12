namespace LabPreTest.Shared.ApiRoutes
{
    public static class ApiRoutes
    {
        public const string Full = "full";
        public const string TotalPages = "totalPages";
        public const string CreateUser = "CreateUser";
        public const string Login = "Login";
        public const string Combo = "Combo";
        public const string ChangePassword = "ChangePassword";

        public const string CountriesRoute = "api/countries";
        public const string CountriesComboRoute = CountriesRoute + $"/{Combo}";
        public const string CitiesRoute = "api/cities";
        public const string CitiesComboRoute = CitiesRoute + $"/{Combo}";
        public const string MedicianRoute = "api/Medics";
        public const string MedicianDocumentRoute = $"{MedicianRoute}/document";
        public const string MedicianFullRoute = MedicianRoute +$"/{Full}";
        public const string StatesRoute = "api/states";
        public const string StatesComboRoute = StatesRoute + $"/{Combo}";
        public const string SectionRoute = "api/Section";
        public const string TemporalOrdersRoute = "api/TemporalOrders";
        public const string TemporalOrdersMyRoute = $"{TemporalOrdersRoute}/my";
        public const string TemporalOrdersFullRoute = $"{TemporalOrdersRoute}/{Full}";
        public const string TestRoute = "api/Test";
        public const string TestFullRoute = $"{TestRoute}/{Full}";
        public const string TestDTORoute = $"{TestRoute}/dto";
        public const string TestTubeRoute = "api/TestTube";
        public const string PatientsRoute = "api/Patients";
        public const string PatientsDocumentRoute = $"{PatientsRoute}/document";
        public const string PatientsFullRoute = $"{PatientsRoute}/{Full}";
        public const string PreanalyticConditionsRoute = "api/PreanalyticConditions";
        public const string PreanalyticConditionsFullRoute= $"{PreanalyticConditionsRoute}/{Full}";
        public const string OrdersRoute = "api/Orders";
        public const string Accounts = "/api/accounts";
        public const string AccountsCreateUser = Accounts + $"/{CreateUser}";
        public const string AccountsLogin = Accounts + $"/{Login}";
        public const string AccountsResendToken = Accounts + "/ResendToken";
    
    }
}