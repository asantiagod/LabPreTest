using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : GenericController<State>
    {
        private readonly IStatesUnitOfWork _statesUnitOfWork;

        public StatesController(IGenericUnitOfWork<State> unitOfWork, IStatesUnitOfWork statesUnitOfWork) : base(unitOfWork)
        {
            _statesUnitOfWork = statesUnitOfWork;
        }

        [HttpGet(ApiRoutes.Full)]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _statesUnitOfWork.GetAsync();
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _statesUnitOfWork.GetAsync(id);
            if (action.WasSuccess)
                return Ok(action.Result);
            return NotFound(action.Message);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Combo + "/{countryId:int}")]
        public async Task<IActionResult> GetComboAsync(int countryId)
        {
            return Ok(await _statesUnitOfWork.GetComboAsync(countryId));
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO paging)
        {
            var action = await _statesUnitOfWork.GetAsync(paging);
            if(action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }

        [HttpGet(ApiRoutes.TotalPages)]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO paging)
        {
            var action = await _statesUnitOfWork.GetTotalPagesAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }
    }
}