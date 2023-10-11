using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Registrar.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student name canot be empty!")]
        public string Name { get; set; }
        public int StudentId { get; set; }
    }
}
