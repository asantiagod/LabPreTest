using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Interfaces
{
    public interface IUserEntity
    {
        string DocumentId { get; set; } 
        string Name { get; set; }
        string BirthDay { get; set; }
        string Cellphone { get; set; }
        string Address { get; set; }
        string Email { get; set; } 
        string UserName {  get; set; }
    }
}
