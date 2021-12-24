using RESTapp.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RESTapp.Dtos
{
[AuthorValidationAttribute]
    public class AuthorModifyDto
    {
        [Required(ErrorMessage = "Firstname harus di isi")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname harus di isi")]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Category harus di isi")]
        [MaxLength(50)]
        public string MainCategory { get; set; }
    }
}
