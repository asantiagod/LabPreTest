using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
builder.Services.AddTransient<SeedDB>();

// Inject enity repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();

builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();

builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();

builder.Services.AddScoped<ITestTubeRepository, TestTubeRepository>();
builder.Services.AddScoped<ITestTubeUnitOfWork, TestTubeUnitOfWork>();

builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientUnitOfWork, PatientUnitOfWork>();

builder.Services.AddScoped<IMedicianRepository, MedicianRepository>();
builder.Services.AddScoped<IMedicianUnitOfWork, MediciansUnitOfWork>();

builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<ISectionUnitOfWork, SectionUnitOfWork>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

// TODO: increase security for production code
builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
}
)
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

SeedData(app);
void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDB>();
        service!.SeedAsync().Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// TODO: validate meaning of this instruction
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
app.Run();