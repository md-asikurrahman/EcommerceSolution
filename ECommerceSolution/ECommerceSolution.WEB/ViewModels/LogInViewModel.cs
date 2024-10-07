using System.ComponentModel.DataAnnotations;

namespace ECommerceSolution.WEB.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string? Password { get; set; }
    }
}
