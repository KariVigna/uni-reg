using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Registrar.Models
{
  public class Department
  {
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public List<DepartmentStudent> JoinEntities { get; }
    public List<CourseDepartment> JoinEntities2 { get; }
  }
}