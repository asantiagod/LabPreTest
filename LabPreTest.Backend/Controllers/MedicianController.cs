using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabPreTest.Backend.Data;
using LabPreTest.Shared.Entities;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Backend.UnitOfWork.Implementations;

namespace LabPreTest.Backend.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicsController : GenericController<Medic>
    {
        private readonly IMedicianUnitOfWork _mediciansUnitOfWork;

        public MedicsController(IGenericUnitOfWork<Medic> unitOfWork, IMedicianUnitOfWork mediciansUnitOfWork) : base(unitOfWork)
        {
            _mediciansUnitOfWork = mediciansUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO pagination)
        {
            var response = await _mediciansUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _mediciansUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _mediciansUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _mediciansUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}

