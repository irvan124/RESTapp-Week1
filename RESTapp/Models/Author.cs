using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTapp.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required (ErrorMessage ="Firstname harus di isi")]
        [MaxLength(50)]
        public string FirstName  { get; set; }
        [Required(ErrorMessage = "Lastname harus di isi")]
        [MaxLength(50)]
        public string LastName { get; set; } 
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Category harus di isi")]
        [MaxLength(50)]
        public string MainCategory { get; set; }
        

      
        public ICollection<Course> Courses { get; set; }
    }
}
