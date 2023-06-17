using System.ComponentModel.DataAnnotations;

namespace required.Modals
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter password")]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "Current pasword")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter new password")]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "New pasword")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm new pasword")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [DataType(dataType: DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="New password and confirm new password does not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
