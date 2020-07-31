using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            EfGenericRepository<StudentPoco> students = 
                new EfGenericRepository<StudentPoco>();

            students.Add(new StudentPoco()
                {
                    Courses = new List<CoursePoco>()
                    { new CoursePoco() {Name = ".Net Bridging"}},
                    Name = "Sally JOnes"
                    
                });


        }
    }

    /*
     * Database
     * - Student
     * - Course
     * - Marks
     * - Teacher
     * 
     * - Student -> many courses
     * - Each course has one teacher
     * - Student has many grades
     */

    public class SchoolContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
           = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<StudentPoco> Students { get; set; }
        public DbSet<CoursePoco> Courses { get; set; }
        public DbSet<TeacherPoco> Teachers { get; set; }
        public DbSet<MarkPoco> Marks { get; set; }

        protected override void 
            OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(MyLoggerFactory).
            UseSqlServer(@"Data Source=CSHARPHUMBER\HUMBERBRIDGING;Initial Catalog=HUMBER_MARKS_DB;Integrated Security=True;");
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarkPoco>(
                entity =>
                entity.HasKey(entity => 
                  new { entity.CourseId, entity.StudentId }));

            base.OnModelCreating(modelBuilder);
        }
    }

    public class StudentPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CoursePoco> Courses { get; set; }
    }

    public class CoursePoco
    {
        [Key]
        public int MumboJumbo { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public TeacherPoco Teacher { get; set; }

    }

    public class TeacherPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MarkPoco : ISoftDelete
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public CoursePoco Course { get; set; }
        public StudentPoco Student { get; set; }
        public int Mark { get; set; }
        public bool AmIDeleted { get; set ; }
    }

    public interface ISoftDelete
    {
        public bool AmIDeleted { get; set; }
    }


}
