using System;
using System.ComponentModel.DataAnnotations;

namespace required.Modals
{
    public class SigUpUserModel
    {
        [Required(ErrorMessage ="Please enter first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name ="Last name")]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage ="Plese enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password",ErrorMessage ="Password and confirm pasword does not match")]
        [DataType(DataType.Password)]


        public string ConfirmPassword { get; set; }
    }
}
