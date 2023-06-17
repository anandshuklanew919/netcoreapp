using System.ComponentModel.DataAnnotations;

namespace required.Modals
{
    public class ResetPassword
    {
        [Required, DataType(DataType.Password), Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm your password")]
        [Compare("Password",ErrorMessage ="Password and confirm does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public bool IsSuccess { get; set; }
    }
}
