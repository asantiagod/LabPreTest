using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CitiesController : GenericController<City>
    {
        private readonly ICitiesUnitOfWork _citiesUnitOfWork;

        public CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : base(unitOfWork)
        {
            _citiesUnitOfWork = citiesUnitOfWork;
        }

        [HttpGet("combo/{countryId:int}")]
        public async Task<IActionResult> GetComboAsync(int countryId)
        {
            return Ok(await _citiesUnitOfWork.GetComboAsync(countryId));
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