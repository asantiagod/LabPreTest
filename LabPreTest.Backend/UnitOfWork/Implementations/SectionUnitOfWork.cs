using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class SectionUnitOfWork : GenericUnitOfWork<Section>, ISectionUnitOfWork
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionUnitOfWork(IGenericRepository<Section> repository, ISectionRepository sectionRepository) : base(repository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO) => await _sectionRepository.AddImageAsync(imageDTO);

        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO) => await _sectionRepository.RemoveLastImageAsync(imageDTO);

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync() => await _sectionRepository.GetAsync();

        public override async Task<ActionResponse<Section>> GetAsync(int id) => await _sectionRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync(PagingDTO paging) => await _sectionRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging) => await _sectionRepository.GetTotalPagesAsync(paging);
    }
}