using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface ISectionUnitOfWork
    {
        Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO);

        Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO);

        Task<ActionResponse<IEnumerable<Section>>> GetAsync();

        Task<ActionResponse<Section>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Section>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}
