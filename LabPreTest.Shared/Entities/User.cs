using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = EntityMessages.UserDocumentDisplayName)]
        [MaxLength(20, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Document { get; set; } = null!;

        [Display(Name = EntityMessages.UserFirstNameDisplayName)]
        [MaxLength(20, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string FirstName { get; set; } = null!;

        [Display(Name = EntityMessages.UserLastNameDisplayName)]
        [MaxLength(20, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string LastName { get; set; } = null!;

        [Display(Name = EntityMessages.UserAddressDisplayName)]
        [MaxLength(200, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Address { get; set; } = null!;

        [Display(Name = EntityMessages.UserPhotoDisplayName)]
        public string? Photo { get; set; }

        [Display(Name = EntityMessages.UserTypeDisplayName)]
        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name = EntityMessages.UserCityDisplayName)]
        [Range(1, int.MaxValue, ErrorMessage = EntityMessages.RangeErrorMessage)]
        public int CityId { get; set; }

        [Display(Name = EntityMessages.UserDisplayName)]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<TemporalOrder>? TemporalOrders { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}