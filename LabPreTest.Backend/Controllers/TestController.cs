using LabPreTest.Backend.UnitOfWork.Implementations;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

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



    }
}
