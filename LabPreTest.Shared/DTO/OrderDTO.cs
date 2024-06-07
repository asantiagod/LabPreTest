using LabPreTest.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
