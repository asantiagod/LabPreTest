using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderAuditsController : GenericAuditsCrontroller<OrderAudit>
    {
        public OrderAuditsController(IGenericAuditUnitOfWork<OrderAudit> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
