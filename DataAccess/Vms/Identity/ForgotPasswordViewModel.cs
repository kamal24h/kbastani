using System.ComponentModel.DataAnnotations;

namespace DataAccess.Vms.Identity
{
    public class ForgotPasswordViewModel {
        [Required(ErrorMessage = "RequiredEmail")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = "Email")]
        public required string Email { get; set; } }
}
