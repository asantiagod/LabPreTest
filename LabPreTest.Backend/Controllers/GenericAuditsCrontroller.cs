using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    public class GenericAuditsCrontroller<T>: Controller where T : class
    {
        private readonly IGenericAuditUnitOfWork<T> _unitOfWork;

        public GenericAuditsCrontroller(IGenericAuditUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(ApiRoutes.Full)]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action  = await _unitOfWork.GetAsync();
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet("/{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var action  = await _unitOfWork.GetAsync(id);
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet]
        public virtual async Task<IActionResult>GetAsync(PagingDTO pagingDTO)
        {
            var action  = await _unitOfWork.GetAsync(pagingDTO);
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet(ApiRoutes.TotalPages)]
        public virtual async Task<IActionResult> GetTotalPagesAsync(PagingDTO paginDTO)
        {
            var action  = await _unitOfWork.GetTotalPagesAsync(paginDTO);
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }
    }
}
