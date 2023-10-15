using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;


namespace Registrar.Models
{
    public class Course
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string CourseNum { get; set; }
        public List<CourseStudent> JoinEntities { get; }
    }
}