using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;
using Moq;

namespace LabPreTest.Test.UnitsOfWork
{
    [TestClass]
    public class PatientUnitOfWorkTest
    {
        private Mock<IGenericRepository<Patient>> _mockGenericRepository = null!;
        private Mock<IPatientRepository> _mockPatientRepository = null!;
        private PatientUnitOfWork _patientUnitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Patient>>();
            _mockPatientRepository = new Mock<IPatientRepository>();
            _patientUnitOfWork = new PatientUnitOfWork(_mockGenericRepository.Object,
                                                       _mockPatientRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryAndReturnsResult()
        {
            var expectedPatients = new List<Patient> { new Patient() };
            var response = new ActionResponse<IEnumerable<Patient>> { WasSuccess = true, Result = expectedPatients };
            _mockPatientRepository.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _patientUnitOfWork.GetAsync();

            Assert.AreEqual(response, result);
            _mockPatientRepository.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByIdAndReturnsResult()
        {
            var patient = new Patient();
            var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
            int id = 1;
            _mockPatientRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(response);

            var result = await _patientUnitOfWork.GetAsync(id);

            Assert.AreEqual(response, result);
            _mockPatientRepository.Verify(x => x.GetAsync(id), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByPaginAndReturnsResult()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Patient>>
            {
                WasSuccess = true,
                Result = new List<Patient>()
            };
            _mockPatientRepository.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientUnitOfWork.GetAsync(pagingDTO);

            Assert.AreEqual(response, result);
            _mockPatientRepository.Verify(x => x.GetAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_CallsRepositoryByPaginAndReturnsResult()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<int>
            {
                WasSuccess = true,
                Result = 5
            };
            _mockPatientRepository.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientUnitOfWork.GetTotalPagesAsync(pagingDTO);

            Assert.AreEqual(response, result);
            _mockPatientRepository.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByDocumentIdAndReturnsResult()
        {
            var patient = new Patient { Name = "Some Patient", DocumentId = "123456" };
            var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
            _mockPatientRepository.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

            var result = await _patientUnitOfWork.GetAsync(patient.DocumentId);

            Assert.AreEqual(response, result);
            _mockPatientRepository.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
        }
    }
}