using System.ComponentModel.DataAnnotations;

namespace ECommerceSolution.WEB.ViewModels
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage = "Please enter your email or username")]
        public string? UserName { get; set; }
        public string? RootUrl { get; set; }
    }
}
