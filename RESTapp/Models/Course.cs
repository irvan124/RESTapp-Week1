using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTapp.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int AuthorID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }


        public Author Authors { get; set; }
    }
}
