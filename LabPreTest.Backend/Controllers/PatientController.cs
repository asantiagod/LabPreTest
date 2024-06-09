using Microsoft.AspNetCore.Mvc;
using LabPreTest.Shared.Entities;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Backend.UnitOfWork.Implementations;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PatientsController : GenericController<Patient>
    {
        private readonly IPatientUnitOfWork _patientUnitOfWork;

        public PatientsController(IGenericUnitOfWork<Patient> unitOfWork, IPatientUnitOfWork patientUnitOfWork) : base(unitOfWork)
        {
            _patientUnitOfWork = patientUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO pagination)
        {
            var response = await _patientUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _patientUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _patientUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("document/{documentId}")]
        public async Task<IActionResult> GetAsync(string documentId)
        {
            var response = await _patientUnitOfWork.GetAsync(documentId);
            if(response.WasSuccess)
                return Ok(response.Result);

            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _patientUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}