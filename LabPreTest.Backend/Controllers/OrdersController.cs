using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers

{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersHelper _ordersHelper;
        private readonly IOrdersUnitOfWork _ordersUnitOfWork;

        public OrdersController(IOrdersHelper ordersHelper, IOrdersUnitOfWork ordersUnitOfWork)
        {
            _ordersHelper = ordersHelper;
            _ordersUnitOfWork = ordersUnitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _ordersUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _ordersUnitOfWork.GetAsync(User.Identity!.Name!, pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _ordersUnitOfWork.GetTotalPagesAsync(User.Identity!.Name!, pagination);

            if (action.WasSuccess)
                return Ok(action.Result);

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var response = await _ordersHelper.ProcessOrderAsync(User.Identity!.Name!);
            if (response.WasSuccess)
                return NoContent();

            return BadRequest(response.Message);
        }

        [HttpPut("details/{id}")]
        public async Task<IActionResult> PutDetailAsync(int id, OrderDetailDTO orderDetailDTO)
        {
            if (orderDetailDTO.OrderId == 0)
                return BadRequest(MessageStrings.DbRecordNotFoundMessage);

            var action = await _ordersUnitOfWork.UpdateAsync(User.Identity!.Name!, id, orderDetailDTO);
            if (action.WasSuccess)
                return Ok();

            return BadRequest(action.Message);
        }
    }
}