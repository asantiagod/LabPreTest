using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace LabPreTest.Test.Repositories
{
    [TestClass]
    public class PatientRepositoryTest
    {
        private Mock<IHttpContextAccessor> _mockContextAccessor = null!;
        private DataContext _dataContext = null!;
        private PatientRepository _patientRepository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            //var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, "mockUser")
            //}, "mock"));
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal();

            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            _dataContext = new DataContext(options, _mockContextAccessor.Object);
            _patientRepository = new PatientRepository(_dataContext);

            _dataContext.Patients.AddRange(new List<Patient>
            {
                new Patient{ Id = 1, DocumentId="123456", Name="user ID_1", Gender=GenderType.Female},
                new Patient{ Id = 2, DocumentId="234561", Name="user ID_2", Gender=GenderType.Male},
                new Patient{ Id = 3, DocumentId="345612", Name="user ID_3", Gender=GenderType.Female},
                new Patient{ Id = 4, DocumentId="456123", Name="user ID_4", Gender=GenderType.Male},
                new Patient{ Id = 5, DocumentId="561234", Name="user ID_5", Gender=GenderType.Female},
                new Patient{ Id = 6, DocumentId="612345", Name="user ID_6", Gender=GenderType.Male},
            });
            _dataContext.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsSuccessfulWhenPatientIsFound()
        {
            // Arrange
            int patientId = 1;

            // Act
            var response = await _patientRepository.GetAsync(patientId);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var patient = response.Result!;
            Assert.AreEqual(patient.Name, "user ID_1");
        }

        [TestMethod]
        public async Task GetAsync_ReturnsErrorMessageWhenPatientIsNotFound()
        {
            // Arrange
            int patientId = 123;

            // Act
            var response = await _patientRepository.GetAsync(patientId);

            // Assert
            Assert.IsFalse(response.WasSuccess);
            var errorMessage = response.Message;
            Assert.AreEqual(errorMessage, MessageStrings.DbRecordNotFoundMessage);
        }
    }
}