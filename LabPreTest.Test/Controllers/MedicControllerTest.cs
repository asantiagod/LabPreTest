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
        private Mock<IGenericUnitOfWork<Medic>> _mockGenericUnitOfWork = null!;
        private Mock<IMedicianUnitOfWork> _mockMedicUnitOfWork = null!;
        private MedicsController _mediciansController = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Medic>>();
            _mockMedicUnitOfWork = new Mock<IMedicianUnitOfWork>();
            _mediciansController = new MedicsController(_mockGenericUnitOfWork.Object,
                                                         _mockMedicUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Medic>> { WasSuccess = true };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadrequest_WhenWasSuccessIsFalse()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Medic>> { WasSuccess = false };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockMedicUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansController.GetPagesAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(response.Result, okResult.Value);
            _mockMedicUnitOfWork?.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequest_WhenWasSuccessIsFalse()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<int> { WasSuccess = false};
            _mockMedicUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansController.GetPagesAsync(pagingDTO);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockMedicUnitOfWork?.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncFull_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var response = new ActionResponse<IEnumerable<Medic>> { WasSuccess = true };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncFull_ReturnsBadrequest_WhenWasSuccessIsFalse()
        {
            var response = new ActionResponse<IEnumerable<Medic>> { WasSuccess = false };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncId_ReturnsOK_WhenWasSuccessIsTrue()
        {
            var patient = new Medic { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Medic> { WasSuccess = true, Result = patient };
            int patientId = 123;
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(patientId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsyncId_ReturnsNotFound_WhenWasSuccessIsFalse()
        {
            var response = new ActionResponse<Medic> { WasSuccess = false };
            int patientId = 123;
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(patientId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOK_WhenDocumentIdMatch()
        {
            var patient = new Medic { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Medic> { WasSuccess = true, Result = patient };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(patient.DocumentId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsNotFound_WhenDocumentIdNotMatch()
        {
            var patient = new Medic { Name = "some name", DocumentId = "123456" };
            var response = new ActionResponse<Medic> { WasSuccess = false, Result = patient };
            _mockMedicUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

            var result = await _mediciansController.GetAsync(patient.DocumentId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _mockMedicUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
        }
    }
}