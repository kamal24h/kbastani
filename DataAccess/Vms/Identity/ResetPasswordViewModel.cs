using System.ComponentModel.DataAnnotations;

namespace DataAccess.Vms.Identity
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredEmail")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredPassword")]
        [StringLength(100, ErrorMessage = "PasswordLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "PasswordMismatch")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }

}
