using System.ComponentModel.DataAnnotations;

namespace ECommerceSolution.WEB.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Required"), DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Required"), DataType(DataType.Password),
         Compare("Password", ErrorMessage = "Confirm password doesn't match with password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? LastName { get; set; }

        public string? CompanyName { get; set; } 

        [Required(ErrorMessage = "Required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? AddressLine { get; set; }


        [Required(ErrorMessage = "Required")]
        public string? PostOffice { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Required")]
        public int ZipCode { get; set; }

        public string? EmailConfirmWebUrl { get; set; }
    }
}
