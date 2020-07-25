using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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

        public DbSet<StudentPoco> Students { get; set; }
        public DbSet<CoursePoco> Courses { get; set; }
        public DbSet<TeacherPoco> Teachers { get; set; }
        public DbSet<MarkPoco> Marks { get; set; }

        protected override void 
            OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=CSHARPHUMBER\HUMBERBRIDGING;Initial Catalog=HUMBER_MARKS_DB;Integrated Security=True;");
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

    public class MarkPoco
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public CoursePoco Course { get; set; }
        public StudentPoco Student { get; set; }
        public int Mark { get; set; }

    }

}
