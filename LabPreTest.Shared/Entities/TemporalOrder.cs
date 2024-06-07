using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Entities
{
    public class TemporalOrder
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public int UserId { get; set; }
        
        public Test? Test { get; set; }
        public int TestId { get; set; }

        public Medic? Medic { get; set; }
        public int MedicId { get; set;}

        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
    }
}