using System.ComponentModel.DataAnnotations;

namespace required.Modals
{
    public class EmailConfirModel
    {
       
        public string Email { get; set; }

        public bool IsConfirmed { get; set; }

        public bool EmailSent { get; set; }

        public bool EmailVerified { get; set; }
    }
}
