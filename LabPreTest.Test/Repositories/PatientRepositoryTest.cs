﻿using LabPreTest.Backend.Data;
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

        [TestMethod]
        public async Task GetAsync_ReturnsList()
        {
            //Arrange

            // Act
            var response = await _patientRepository.GetAsync();

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var list = response.Result!;
            Assert.AreEqual(6, list.Count());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAFilteredMedic()
        {
            // Arrange
            var pagingDTO = new PagingDTO { Filter = "testpatient" };
            var patient = new Patient { DocumentId = "1122334455", Name = "TestPatient", Gender = GenderType.Female };
            _dataContext.Patients.Add(patient);
            _dataContext.SaveChanges();

            // Act
            var response = await _patientRepository.GetAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            var filteredPatients = response.Result!;
            Assert.AreEqual(1, filteredPatients.Count());
            Assert.AreEqual(patient, filteredPatients.First());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNonFilteredList()
        {
            // Arrange
            var pagingDTO = new PagingDTO();

            // Act
            var response = await _patientRepository.GetAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            var filteredPatients = response.Result!;
            Assert.AreEqual(6, filteredPatients.Count());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsTotalPages()
        {
            // Arrange
            var pagingDTO = new PagingDTO();

            // Act
            var response = await _patientRepository.GetTotalPagesAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsTotalPagesForLimitRecordsNumber()
        {
            // Arrange
            var pagingDTO = new PagingDTO { Filter = "user",RecordsNumber = 1 };

            // Act
            var response = await _patientRepository.GetTotalPagesAsync(pagingDTO);

            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(6, response.Result);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsSuccessByDocumentId()
        {
            // Arrange
            string documentId = "123456";

            // Act
            var response = await _patientRepository.GetAsync(documentId);

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
            var response = await _patientRepository.GetAsync(documentId);

            Assert.IsFalse(response.WasSuccess);
        }

    }
}