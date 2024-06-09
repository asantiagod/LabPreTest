using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Entities
{
    public class SectionImage
    {
        public int Id { get; set; }

        public Section? Section { get; set; }

        public int SectionId { get; set; }

        [Display(Name = "Imagen")]
        public string Image { get; set; } = null!;
    }
    
}
