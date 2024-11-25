//using LabPreTest.Backend.Controllers;
//using LabPreTest.Backend.UnitOfWork.Interfaces;
//using LabPreTest.Shared.DTO;
//using LabPreTest.Shared.Entities;
//using LabPreTest.Shared.Responses;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LabPreTest.Test.Controllers
//{
//    [TestClass]
//    public class TemporalOrdersControllerTest
//    {
//        private Mock<IGenericUnitOfWork<TemporalOrder>> _mockGenericUnitOfWork = null!;
//        private Mock<ITemporalOrdersUnitOfWork> _mockTemporalOrderUnitOfWork = null!;
//        private TemporalOrdersController _temporalOrderController  = null!;

//        [TestInitialize]
//        public void Setup()
//        {
//            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<TemporalOrder>>();
//            _mockTemporalOrderUnitOfWork = new Mock<ITemporalOrdersUnitOfWork>();
//            _temporalOrderController = new TemporalOrdersController(_mockGenericUnitOfWork.Object,
//                                                                    _mockTemporalOrderUnitOfWork.Object);
//        }

//        [TestMethod]
//        public async Task GetAsyncFull_ReturnsOK_WhenWasSuccessIsTrue()
//        {
//            var response = new ActionResponse<IEnumerable<TemporalOrder>> { WasSuccess = true };
//            string userEmail = "some.user@yopmail.com";
//            _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(userEmail)).ReturnsAsync(response);

//            var result = await _temporalOrderController.GetAsync();

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var okResult = result as OkObjectResult;
//            Assert.AreEqual(response.Result, okResult!.Value);
//            _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(userEmail), Times.Once());
//        }

//        [TestMethod]
//        public async Task GetAsyncFull_ReturnsBadrequest_WhenWasSuccessIsFalse()
//        {
//            var response = new ActionResponse<IEnumerable<TemporalOrder>> { WasSuccess = false };
//            string userEmail = "some.user@yopmail.com";
//            _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(userEmail)).ReturnsAsync(response);

//            var result = await _temporalOrderController.GetAsync();

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
//            _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(userEmail), Times.Once());
//        }

//        //[TestMethod]
//        //public async Task GetAsyncId_ReturnsOK_WhenWasSuccessIsTrue()
//        //{
//        //    var patient = new Patient { Name = "some name", DocumentId = "123456" };
//        //    var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
//        //    int patientId = 123;
//        //    _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

//        //    var result = await _temporalOrderController.GetAsync(patientId);

//        //    // Assert
//        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//        //    var okResult = result as OkObjectResult;
//        //    Assert.AreEqual(response.Result, okResult!.Value);
//        //    _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
//        //}

//        //[TestMethod]
//        //public async Task GetAsyncId_ReturnsNotFound_WhenWasSuccessIsFalse()
//        //{
//        //    var response = new ActionResponse<Patient> { WasSuccess = false };
//        //    int patientId = 123;
//        //    _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(patientId)).ReturnsAsync(response);

//        //    var result = await _temporalOrderController.GetAsync(patientId);

//        //    // Assert
//        //    Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
//        //    _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(patientId), Times.Once());
//        //}

//        //[TestMethod]
//        //public async Task GetAsync_ReturnsOK_WhenDocumentIdMatch()
//        //{
//        //    var patient = new Patient { Name = "some name", DocumentId = "123456" };
//        //    var response = new ActionResponse<Patient> { WasSuccess = true, Result = patient };
//        //    _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

//        //    var result = await _temporalOrderController.GetAsync(patient.DocumentId);

//        //    // Assert
//        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//        //    var okResult = result as OkObjectResult;
//        //    Assert.AreEqual(response.Result, okResult!.Value);
//        //    _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
//        //}

//        //[TestMethod]
//        //public async Task GetAsync_ReturnsNotFound_WhenDocumentIdNotMatch()
//        //{
//        //    var patient = new Patient { Name = "some name", DocumentId = "123456" };
//        //    var response = new ActionResponse<Patient> { WasSuccess = false, Result = patient };
//        //    _mockTemporalOrderUnitOfWork.Setup(x => x.GetAsync(patient.DocumentId)).ReturnsAsync(response);

//        //    var result = await _temporalOrderController.GetAsync(patient.DocumentId);

//        //    // Assert
//        //    Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
//        //    _mockTemporalOrderUnitOfWork?.Verify(x => x.GetAsync(patient.DocumentId), Times.Once());
//        //}
//    }
//}
