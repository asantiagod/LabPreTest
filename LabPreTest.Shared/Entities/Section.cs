using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Section : IEntityWithId, IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.SectionDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        public ICollection<Test>? Tests { get; set; }
        public int TestNumber => Tests == null || Tests.Count == 0 ? 0 : Tests.Count;
        public ICollection<SectionImage>? SectionImages { get; set; }

        [Display(Name = "Imágenes")]
        public int ProductImagesNumber => SectionImages == null || SectionImages.Count == 0 ? 0 : SectionImages.Count;

        [Display(Name = "Imagén")]
        public string MainImage => SectionImages == null || SectionImages.Count == 0 ? string.Empty : SectionImages.FirstOrDefault()!.Image;
    
    }
}