using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LabPreTest.Tests.Repositories
{
    [TestClass]
    public class OrdersRepositoryTest
    {
        private DataContext _dataContext = null!;
        private OrdersRepository _ordersRepository = null!;
        private Mock<IUsersRepository> _mockUserRepository = null!;
        private Mock<IHttpContextAccessor> _mockContextAccessor = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var httpContext = new DefaultHttpContext();
            _mockContextAccessor = new Mock<IHttpContextAccessor>();
            _mockContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            _dataContext = new DataContext(options, _mockContextAccessor.Object);
            _mockUserRepository = new Mock<IUsersRepository>();
            _ordersRepository = new OrdersRepository(_dataContext, _mockUserRepository.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_UserDoesNotExist_ReturnsFailActionResponse()
        {
            // Act
            var response = await _ordersRepository.GetAsync("nonexistenuser@example.com", new PagingDTO());

            // Assert
            Assert.IsFalse(response.WasSuccess);
            Assert.AreEqual("Usuario no valido", response.Message);
        }

        [TestMethod]
        public async Task GetAsync_ValidUserAndOrder_ReturnsOrders()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            await CreateTestOrderAsync(user);
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.Admin.ToString()))
                               .ReturnsAsync(false);

            // Act
            var response = await _ordersRepository.GetAsync(email, new PagingDTO());

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            var orders = response.Result;
            Assert.AreEqual(1, orders.Count());
            _mockUserRepository.Verify(x => x.GetUserAsync(email), Times.Once());
            _mockUserRepository.Verify(x => x.IsUserInRoleAsync(user, UserType.Admin.ToString()),
                                       Times.Once());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_UserDoesNotExist_ResturnFailActionResponse()
        {
            // Act
            var response = await _ordersRepository.GetTotalPagesAsync("nonexistinguser@example.com",
                                                                      new PagingDTO());
            // Assert
            Assert.IsFalse(response.WasSuccess);
            Assert.AreEqual("Usuario no valido", response.Message);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ResturnCorrectNumberOfPages()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            for (var i = 0; i < 26; i++)
                await CreateTestOrderAsync(user);
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.Admin.ToString()))
                               .ReturnsAsync(false);

            // Act
            var response = await _ordersRepository.GetTotalPagesAsync(email,
                                                                  new PagingDTO { RecordsNumber = 5 });
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var nPages = response.Result;
            Assert.AreEqual(6, nPages); // ceil(26 / 5) = 6
        }

        [TestMethod]
        public async Task GetAsync_ById_ReturnsFailActionResponse()
        {
            // Act
            var response = await _ordersRepository.GetAsync(666);

            // Assert
            Assert.IsFalse(response.WasSuccess);
            Assert.AreEqual("La orden no existe", response.Message);
        }

        [TestMethod]
        public async Task GetAsync_ById_ReturnsSuccessActionResponse()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderAsync(user);

            // Act
            var response = await _ordersRepository.GetAsync(1);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(order.Id, response.Result.Id);
        }

        [TestMethod]
        public async Task UpdateAsync_UserWithoutPermissions_ReturnsPermissionError()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var orderDetailDTO = new OrderDetailDTO { OrderId = 1 };
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false); // No es admin ni usuario válido

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.UserDoesNotHavePermissions, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_OrderNotFound_ReturnsDbParameterNotFoundMessage()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var orderDetailDTO = new OrderDetailDTO { OrderId = 666 };
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.DbParameterNotFoundMessage, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_DetailNotFound_ReturnsDbParameterNotFoundMessage()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderAsync(user);
            var orderDetailDTO = new OrderDetailDTO { OrderId = order.Id };
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 999, orderDetailDTO);  // Non-existent detail

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.DbParameterNotFoundMessage, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_TestNotFound_ReturnsDbRecordNotFoundMessage()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderWithDetailsAsync(user);
            var orderDetailDTO = new OrderDetailDTO { OrderId = order.Id, TestId = 999 };  // Non-existent Test ID
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.DbRecordNotFoundMessage, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_MedicNotFound_ReturnsDbRecordNotFoundMessage()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderWithDetailsAsync(user);
            var orderDetailDTO = new OrderDetailDTO { OrderId = order.Id, MedicId = 999 };  // Non-existing Medic ID
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.DbRecordNotFoundMessage, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_PatientNotFound_ReturnsDbRecordNotFoundMessage()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderWithDetailsAsync(user);
            var orderDetailDTO = new OrderDetailDTO { OrderId = order.Id, PatientId = 999 };  // Non-existing patient ID
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual(MessageStrings.DbRecordNotFoundMessage, result.Message);
        }

        [TestMethod]
        public async Task UpdateAsync_SuccessfulUpdate_ReturnsSuccessfulResponse()
        {
            // Arrange
            var email = "test@example.com";
            var user = await CreateTestUserAsync(email, UserType.User);
            var order = await CreateTestOrderWithDetailsAsync(user);
            var orderDetailDTO = new OrderDetailDTO
            {
                OrderId = order.Id,
                TestId = 1,  // valid Test
                MedicId = 1, // valid Medic
                PatientId = 1, // valid Patient
                Status = OrderStatus.OrdenFinalizada
            };
            _mockUserRepository.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.IsUserInRoleAsync(user, UserType.User.ToString())).ReturnsAsync(true);

            // Act
            var result = await _ordersRepository.UpdateAsync(email, 1, orderDetailDTO);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(OrderStatus.OrdenFinalizada, order.Status);  // Verificar que el estado de la orden haya cambiado
        }

        private async Task<User> CreateTestUserAsync(string email, UserType userType)
        {
            var user = new User
            {
                Email = email,
                UserType = userType,
                Address = "Any",
                Document = "Any",
                FirstName = "John",
                LastName = "Doe"
            };

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        private async Task<Order> CreateTestOrderAsync(User user)
        {
            var order = new Order { User = user };
            await _dataContext.Orders.AddAsync(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }

        private async Task<Order> CreateTestOrderWithDetailsAsync(User user)
        {
            var section = new Section
            {
                Id = 1,
                Name = "test-section"
            };
            var condition = new PreanalyticCondition
            {
                Id = 1,
                Name = "test-condition",
                Description = "test-description"
            };
            var tube = new TestTube
            {
                Id = 1,
                Name = "test-tube",
                Description = "test-tube-description"
            };

            var test = new Shared.Entities.Test
            {
                Id = 1,
                TestID = 1,
                Name = "test-test",
                Section = section,
                TestTube = tube,
                Conditions = [condition],
            };

            await _dataContext.Tests.AddAsync(test);
            await _dataContext.SaveChangesAsync();

            var patient = new Patient
            {
                Id = 1,
                DocumentId = "123456",
                Name = "test-patient",
                Gender = GenderType.Male,
            };

            var medic = new Medic
            {
                Id = 1,
                DocumentId = "654321",
                Name = "test-medic",
                Gender = GenderType.Male,
            };

            var order = new Order
            {
                User = user,
                Status = OrderStatus.OrdenCreada,
                Details = [new OrderDetail {
                    Test=test,
                    Medic=medic,
                    Patient=patient,
                    Status=OrderStatus.OrdenCreada}],
            };

            await _dataContext.Orders.AddAsync(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }
    }
}