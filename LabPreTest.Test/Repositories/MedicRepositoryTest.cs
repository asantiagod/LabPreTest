using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Shared.DTO;
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
    public class MedicRepositoryTest
    {
        private Mock<IHttpContextAccessor> _mockContextAccessor = null!;
        private DataContext _dataContext = null!;
        private MedicianRepository _medicianRepository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal();

            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            _dataContext = new DataContext(options, _mockContextAccessor.Object);
            _medicianRepository = new MedicianRepository(_dataContext);

            _dataContext.Medicians.AddRange(new List<Medic>
            {
                new Medic { Id = 1, DocumentId="123456", Name="user ID_1", Gender=GenderType.Female},
                new Medic { Id = 2, DocumentId="234561", Name="user ID_2", Gender=GenderType.Male},
                new Medic { Id = 3, DocumentId="345612", Name="user ID_3", Gender=GenderType.Female},
                new Medic { Id = 4, DocumentId="456123", Name="user ID_4", Gender=GenderType.Male},
                new Medic { Id = 5, DocumentId="561234", Name="user ID_5", Gender=GenderType.Female},
                new Medic { Id = 6, DocumentId="612345", Name="user ID_6", Gender=GenderType.Male},
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
            int medicId = 1;

            // Act
            var response = await _medicianRepository.GetAsync(medicId);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var medic = response.Result!;
            Assert.AreEqual(medic.Name, "user ID_1");
        }

        [TestMethod]
        public async Task GetAsync_ReturnsErrorMessageWhenPatientIsNotFound()
        {
            // Arrange
            int medicId = 123;

            // Act
            var response = await _medicianRepository.GetAsync(medicId);

            // Assert
            Assert.IsFalse(response.WasSuccess);
            var errorMessage = response.Message;
            Assert.AreEqual(errorMessage, MessageStrings.DbMedicianNotFoundMessage);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsList()
        {
            //Arrange

            // Act
            var response = await _medicianRepository.GetAsync();

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var list = response.Result!;
            Assert.AreEqual(6, list.Count());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAFilteredMedic()
        {
            // Arrange
            var pagingDTO = new PagingDTO { Filter = "testmedic" };
            var medic = new Medic { DocumentId = "1122334455", Name = "TestMedician", Gender = GenderType.Female };
            _dataContext.Medicians.Add(medic);
            _dataContext.SaveChanges();

            // Act
            var response = await _medicianRepository.GetAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            var filteredMedics = response.Result!;
            Assert.AreEqual(1, filteredMedics.Count());
            Assert.AreEqual(medic, filteredMedics.First());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNonFilteredList()
        {
            // Arrange
            var pagingDTO = new PagingDTO();

            // Act
            var response = await _medicianRepository.GetAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            var filteredMedics = response.Result!;
            Assert.AreEqual(6, filteredMedics.Count());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsTotalPages()
        {
            // Arrange
            var pagingDTO = new PagingDTO();

            // Act
            var response = await _medicianRepository.GetTotalPagesAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsTotalPagesForLimitRecordsNumber()
        {
            // Arrange
            var pagingDTO = new PagingDTO { Filter = "user",RecordsNumber = 1 };

            // Act
            var response = await _medicianRepository.GetTotalPagesAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(6, response.Result);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsSuccessByDocumentId()
        {
            // Arrange
            string documentId = "123456";

            // Act
            var response = await _medicianRepository.GetAsync(documentId);

            Assert.IsTrue(response.WasSuccess);
            var resultMedic = response.Result!;
            Assert.AreEqual(documentId, resultMedic.DocumentId);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFailByDocumentId()
        {
            // Arrange
            string documentId = "111111";

            // Act
            var response = await _medicianRepository.GetAsync(documentId);

            Assert.IsFalse(response.WasSuccess);
        }
    }
}