using System.ComponentModel.DataAnnotations;
using System;

namespace Registrar.Models
{
  public class CourseDepartment
  {
    public int CourseDepartmentId { get; set; }
    public int DepartmentId { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }

  }
}