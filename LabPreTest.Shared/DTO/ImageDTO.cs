using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.DTO
{
    public class ImageDTO
    {
        [Required] public int SectionID { get; set; }
        [Required] public List<string> Images { get; set; } = null!;
    }

}
