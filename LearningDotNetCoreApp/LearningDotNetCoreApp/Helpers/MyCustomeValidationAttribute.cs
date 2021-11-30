using System.ComponentModel.DataAnnotations;

namespace LearningDotNetCoreApp.Helpers
{
    public class MyCustomeValidationAttribute : ValidationAttribute
    {
        public MyCustomeValidationAttribute(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //validationContext.
            var bookName = value.ToString();
            if (bookName.Contains(Text))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Book name does not contain the desired value");
        }
    }
}
