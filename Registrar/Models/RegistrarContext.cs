using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Registrar.Models;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;

namespace Registrar.Models
{
    public class RegistrarContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseDepartment> CourseDepartments { get; set; }
        public DbSet<DepartmentStudent> DepartmentStudents { get; set; }
        public RegistrarContext(DbContextOptions options) : base(options) { }
    }
}