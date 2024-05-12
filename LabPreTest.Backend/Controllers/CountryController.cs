using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        [HttpGet(ApiRoutes.Full)]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if(response.WasSuccess)
                return Ok(response.Result);
            return BadRequest();
        }

        [HttpGet("{Id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _countriesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO paging)
        {
            var action = await _countriesUnitOfWork.GetAsync(paging);
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet(ApiRoutes.TotalPages)]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO paging)
        {
            var action = await _countriesUnitOfWork.GetTotalPagesAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Combo)]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok( await _countriesUnitOfWork.GetComboAsync());
        }
    }
}