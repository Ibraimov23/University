using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;
using University.Models.Events;

namespace University.Data
{

    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
           
            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasDiscriminator<int>("EventType")
                .HasValue<Lesson>(1)
                .HasValue<Webinar>(2)
                .HasValue<Exam>(3)
                .HasValue<LabWork>(4);
        }
       
        public DbSet<University.Models.Events.Event> Event { get; set; }
       
        public DbSet<University.Models.Events.Lesson> Lesson { get; set; }
       
        public DbSet<University.Models.Events.Webinar> Webinar { get; set; }
       
        public DbSet<University.Models.Events.Exam> Exam { get; set; }
       
        public DbSet<University.Models.Events.LabWork> LabWork { get; set; }
    }

}
