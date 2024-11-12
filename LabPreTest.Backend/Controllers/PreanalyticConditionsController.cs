using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreanalyticConditionsController : GenericController<PreanalyticCondition>
    {
        public PreanalyticConditionsController(IGenericUnitOfWork<PreanalyticCondition> unitOfWork) : base(unitOfWork)
        {
        }
    }
}