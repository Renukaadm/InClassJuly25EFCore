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
                   
                    Name = "Sally Jones"
                    
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

            modelBuilder.Entity<Students_Courses>(
                e => e.
                HasKey(k => new { k.Course.MumboJumbo, k.Student.Id }));

            #region TeacherTable

            modelBuilder.Entity<TeacherPoco>(
                    e => e.Property(p => p.Name).
                    HasMaxLength(50)
                );

            modelBuilder.Entity<MarkPoco>(
                e => e.Property(
                    p => p.TimeStamp).
                    IsRowVersion().
                    IsConcurrencyToken());

            #endregion TeacherTable


            base.OnModelCreating(modelBuilder);
        }
    }

    public class StudentPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public decimal Age { get; set; }
        public List<Students_Courses> StudentCourses { get; set; }
    }

    public class Students_Courses
    {
        
        //public int StudentId { get; set; }
        //public int CourseId { get; set; }
        public StudentPoco Student { get; set; }
        public CoursePoco Course { get; set; }
    }

    public class CoursePoco
    {
        [Key]
        public int MumboJumbo { get; set; }
        //public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public TeacherPoco Teacher { get; set; }
        public List<Students_Courses> StudentCourses { get; set; }

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
        public byte[] TimeStamp { get; set; }
        public bool AmIDeleted { get; set ; }
    }

    public interface ISoftDelete
    {
        public bool AmIDeleted { get; set; }
    }


}
