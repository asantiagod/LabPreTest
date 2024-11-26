using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;
using Moq;

namespace LabPreTest.Test.UnitsOfWork
{
    [TestClass]
    public class MedicUnitOfWorkTest
    {
        private Mock<IGenericRepository<Medic>> _mockGenericRepository = null!;
        private Mock<IMedicianRepository> _mockMediciansRepository = null!;
        private MediciansUnitOfWork _mediciansUnitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Medic>>();
            _mockMediciansRepository = new Mock<IMedicianRepository>();
            _mediciansUnitOfWork = new MediciansUnitOfWork(_mockGenericRepository.Object,
                                                       _mockMediciansRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryAndReturnsResult()
        {
            var expectedMedicians = new List<Medic> { new Medic() };
            var response = new ActionResponse<IEnumerable<Medic>> { WasSuccess = true, Result = expectedMedicians };
            _mockMediciansRepository.Setup(x => x.GetAsync()).ReturnsAsync(response);

            var result = await _mediciansUnitOfWork.GetAsync();

            Assert.AreEqual(response, result);
            _mockMediciansRepository.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByIdAndReturnsResult()
        {
            var patient = new Medic();
            var response = new ActionResponse<Medic> { WasSuccess = true, Result = patient };
            int id = 1;
            _mockMediciansRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(response);

            var result = await _mediciansUnitOfWork.GetAsync(id);

            Assert.AreEqual(response, result);
            _mockMediciansRepository.Verify(x => x.GetAsync(id), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByPaginAndReturnsResult()
        {
            var pagingDTO = new PagingDTO();
            var response = new ActionResponse<IEnumerable<Medic>>
            {
                WasSuccess = true,
                Result = new List<Medic>()
            };
            _mockMediciansRepository.Setup(x => x.GetAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansUnitOfWork.GetAsync(pagingDTO);

            Assert.AreEqual(response, result);
            _mockMediciansRepository.Verify(x => x.GetAsync(pagingDTO), Times.Once());
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
            _mockMediciansRepository.Setup(x => x.GetTotalPagesAsync(pagingDTO)).ReturnsAsync(response);

            var result = await _mediciansUnitOfWork.GetTotalPagesAsync(pagingDTO);

            Assert.AreEqual(response, result);
            _mockMediciansRepository.Verify(x => x.GetTotalPagesAsync(pagingDTO), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryByDocumentIdAndReturnsResult()
        {
            var medic = new Medic { Name = "Some Medic", DocumentId = "123456" };
            var response = new ActionResponse<Medic> { WasSuccess = true, Result = medic };
            _mockMediciansRepository.Setup(x => x.GetAsync(medic.DocumentId)).ReturnsAsync(response);

            var result = await _mediciansUnitOfWork.GetAsync(medic.DocumentId);

            Assert.AreEqual(response, result);
            _mockMediciansRepository.Verify(x => x.GetAsync(medic.DocumentId), Times.Once());
        }
    }
}