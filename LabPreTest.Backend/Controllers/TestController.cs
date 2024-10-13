using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : GenericController<Test>
    {
        private readonly ITestUnitOfWork _testsUnitOfWork;

        public TestController(IGenericUnitOfWork<Test> unitOfWork, ITestUnitOfWork testUnitOfWork) : base(unitOfWork)
        {
            _testsUnitOfWork = testUnitOfWork;
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _testsUnitOfWork.DeleteAsync(id);
            if (action.WasSuccess)
                return NoContent();
            return BadRequest(action.Message);
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO pagination)
        {
            var response = await _testsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _testsUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _testsUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [AllowAnonymous]
        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _testsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("dto")]
        public async Task<IActionResult> PostAsync([FromBody] TestDTO testDTO)
        {
            var action = await _testsUnitOfWork.AddAsync(testDTO);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest(action.Message);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TestDTO testDTO)
        {
            var action = await _testsUnitOfWork.UpdateAsync(id, testDTO);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest(action.Message);
        }
    }
}