using RESTapp.Dtos;
using System.ComponentModel.DataAnnotations;

namespace RESTapp.ValidationAttributes
{
    public class AuthorValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var author = (AuthorModifyDto)validationContext.ObjectInstance;
            if (author.FirstName == author.LastName)
            {
                return new ValidationResult("Firstname dan Lastname tidak boleh sama", new[] { nameof(AuthorModifyDto) });
            }
            return ValidationResult.Success;
        }
    }
}
