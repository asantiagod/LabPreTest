using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        private readonly ICitiesUnitOfWork _citiesUnitOfWork;

        public CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : base(unitOfWork)
        {
            _citiesUnitOfWork = citiesUnitOfWork;
        }

        [HttpGet(ApiRoutes.Full)]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _citiesUnitOfWork.GetAsync();
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _citiesUnitOfWork.GetAsync(id);

            if (action.WasSuccess)
                return Ok(action.Result);

            return NotFound(action.Message);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO paging)
        {
            var action = await _citiesUnitOfWork.GetAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet(ApiRoutes.TotalPages)]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO paging)
        {
            var action = await _citiesUnitOfWork.GetTotalPagesAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }
    }
}