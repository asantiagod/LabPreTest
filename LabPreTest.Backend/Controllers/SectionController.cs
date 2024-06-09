using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : GenericController<Section>
    {
        private readonly ISectionUnitOfWork _sectionUnitOfWork;

        public SectionController(IGenericUnitOfWork<Section> unitOfWork, ISectionUnitOfWork testUnitOfWork) : base(unitOfWork)
        {
            _sectionUnitOfWork = testUnitOfWork;
        }
        [HttpPost("addImages")]
        public async Task<IActionResult> PostAddImagesAsync(ImageDTO imageDTO)
        {
            var action = await _sectionUnitOfWork.AddImageAsync(imageDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPost("removeLastImage")]
        public async Task<IActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
        {
            var action = await _sectionUnitOfWork.RemoveLastImageAsync(imageDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PagingDTO pagination)
        {
            var response = await _sectionUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _sectionUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _sectionUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO pagination)
        {
            var action = await _sectionUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}