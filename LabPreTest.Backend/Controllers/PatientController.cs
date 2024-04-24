using Microsoft.AspNetCore.Mvc;
using LabPreTest.Shared.Entities;
using LabPreTest.Backend.UnitOfWork.Interfaces;

namespace LabPreTest.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PatientsController : GenericController<Patient>
    {

        public PatientsController(IGenericUnitOfWork<Patient> unitOfWork) : base(unitOfWork)
        {
        }
    }
}