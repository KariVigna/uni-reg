using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
    public class StudentsController : Controller
    {
        private readonly RegistrarContext _db;
        public StudentsController(RegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Students.ToList());
        }

        public ActionResult Create()
        {
            // ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
        Student thisStudent = _db.Students
        //may need to change this line
            .Include(student => student.JoinEntities)
            .ThenInclude(join => join.Course)
            .FirstOrDefault(student => student.StudentId == id);
        return View(thisStudent);
        }

        public ActionResult AddCourse(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult AddCourse(Student student, int courseId)
        {
            #nullable enable
            CourseStudent? joinEntity = _db.CourseStudents.FirstOrDefault(join => (join.CourseId == courseId && join.StudentId == student.StudentId));
            #nullable disable
            if (joinEntity == null && courseId != 0)
            {
                _db.CourseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = student.StudentId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = student.StudentId });
        }
        
        public ActionResult Edit(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            _db.Students.Update(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            return View(thisStudent);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            _db.Students.Remove(thisStudent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        }
    }