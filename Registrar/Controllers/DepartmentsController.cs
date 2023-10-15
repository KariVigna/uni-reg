using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly RegistrarContext _db;

    public DepartmentsController(RegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Department> model = _db.Departments.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Department department)
    {
      _db.Departments.Add(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddStudent(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(departments => departments.DepartmentId == id);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Name");
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult AddStudent(Department department, int studentId)
    {
      #nullable enable
      DepartmentStudent? joinEntity = _db.DepartmentStudents.FirstOrDefault(join => (join.StudentId == studentId && join.DepartmentId == department.DepartmentId));
      #nullable disable
      if (joinEntity == null && studentId != 0)
      {
        _db.DepartmentStudents.Add(new DepartmentStudent() { StudentId = studentId, DepartmentId = department.DepartmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("StudentDetails", new { id = department.DepartmentId });
    }  

    public ActionResult AddCourse(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult AddCourse(Department department, int courseId)
    {
      #nullable enable
      CourseDepartment? joinEntity = _db.CourseDepartments.FirstOrDefault(join => (join.CourseId == courseId && join.DepartmentId == department.DepartmentId));
      #nullable disable
      if (joinEntity == null && courseId != 0)
      {
        _db.CourseDepartments.Add(new CourseDepartment() { CourseId = courseId, DepartmentId = department.DepartmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("CourseDetails", new { id = department.DepartmentId });
    }  

    public ActionResult StudentDetails(int id)
    {
      Department thisDepartment = _db.Departments
                            .Include(department => department.JoinEntities)
                            .ThenInclude(join => join.Student)
                            .FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    public ActionResult CourseDetails (int id)
    {
      Department thisDepartment = _db.Departments
                                    .Include(department => department.JoinEntities2)
                                    .ThenInclude(join => join.Course)
                                    .FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    public ActionResult Edit(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult Edit(Department department)
    {
      _db.Departments.Update(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      _db.Departments.Remove(thisDepartment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(department => department.DepartmentId == id);
      _db.Departments.Remove(thisDepartment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}