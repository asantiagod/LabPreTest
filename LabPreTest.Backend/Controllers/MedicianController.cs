using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabPreTest.Backend.Data;
using LabPreTest.Shared.Entities;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MediciansController : GenericController<Medician>
    {
        public MediciansController(IGenericUnitOfWork<Medician> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
