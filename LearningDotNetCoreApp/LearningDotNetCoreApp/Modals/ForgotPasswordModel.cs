using System.ComponentModel.DataAnnotations;

namespace required.Modals
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress(ErrorMessage ="Please enter valid email address"), Display(Name ="Registered email address")]
        public string Email { get; set; }

        public bool IsEmailSent { get; set; }
    }
}
