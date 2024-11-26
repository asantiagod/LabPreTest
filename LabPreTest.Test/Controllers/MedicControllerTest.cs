using LabPreTest.Backend.Controllers;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LabPreTest.Test.Controllers
{
    [TestClass]
    public class MedicControllerTest
    {
        private Mock<IGenericUnitOfWork<Patient>> _mockGenericUnitOfWork = null!;
        private Mock<IPatientUnitOfWork> _mockPatientUnitOfWork = null!;
        private PatientsController _patientsController = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Patient>>();
            _mockPatientUnitOfWork = new Mock<IPatientUnitOfWork>();
            _patientsController = new PatientsController(_mockGenericUnitOfWork.Object,
                                                         _mockPatientUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Patient>> { WasSuccess = true };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadrequest_WhenWasSuccessIsFalse()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Patient>> { WasSuccess = false };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockPatientUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientsController.GetPagesAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(response.Result, okResult.Value);
            _mockPatientUnitOfWork?.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequest_WhenWasSuccessIsFalse()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<int> { WasSuccess = false};
            _mockPatientUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _patientsController.GetPagesAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockPatientUnitOfWork?.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncFull_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var response = new ActionResponse<IEnumerable<Patient>> { WasSuccess = true };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _patientsController.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncFull_ReturnsBadrequest_WhenWasSuccessIsFalse()
        {
            var response = new ActionResponse<IEnumerable<Patient>> { WasSuccess = false };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _patientsController.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncId_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var patient = new Patient { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
            int patientId = 123;
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(patientId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncId_ReturnsNotFound_WhenWasSuccessIsFalse()
        {
            var response = new ActionResponse<Patient> { WasSuccess = false };
            int patientId = 123;
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(patientId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOK_WhenDocumentIdMatch()
        {
            var patient = new Patient { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(patient.DocumentId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFound_WhenDocumentIdNotMatch()
        {
            var patient = new Patient { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Patient> { WasSuccess = false, Result = patient };
            _mockPatientUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

            var result = await _patientsController.GetAsync(patient.DocumentId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _mockPatientUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
        }
    }
}