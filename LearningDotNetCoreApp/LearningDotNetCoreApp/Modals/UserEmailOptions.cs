using System.Collections.Generic;

namespace required.Modals
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<KeyValuePair<string, string>> PlaceHolder { get; set; }
        System.ComponentModel.DataAnnotations.ValidationResult
    }
}
