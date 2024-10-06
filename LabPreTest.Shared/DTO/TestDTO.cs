using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.DTO
{
    public class TestDTO
    {
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = EntityMessages.RangeErrorMessage)]
        public int TestID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = EntityMessages.RangeErrorMessage)]
        public int SectionID { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int TestTubeID { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public ICollection<int> Conditions { get; set; } = null!;
    }
}