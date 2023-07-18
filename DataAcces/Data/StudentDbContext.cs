using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Depart = "Business Administration"
                },
                new Department
                {
                    Id = 2,
                    Depart = "Computer Science"
                },
                new Department
                {
                    Id = 3,
                    Depart = "Management Information Systems"
                }
                );

            modelBuilder.Entity<Student>().HasData(
               new Student
               {
                   Id = 1,
                   Name = "John",
                   Surname = "Doe",
                   StudentNumber = 123456,
                   Age = 20,
                   ImageUrl = "",
                   DepartmentId = 1
               },
               new Student
               {
                   Id = 2,
                   Name = "Jane",
                   Surname = "Doe",
                   StudentNumber = 654321,
                   Age = 21,
                   ImageUrl = "",
                   DepartmentId = 1
               },
               new Student
               {
                   Id = 3,
                   Name = "John",
                   Surname = "Smith",
                   StudentNumber = 123654,
                   Age = 22,
                   ImageUrl = "",
                   DepartmentId = 1
               },
               new Student
               {
                   Id = 4,
                   Name = "Jane",
                   Surname = "Smith",
                   StudentNumber = 321654,
                   Age = 23,
                   ImageUrl = "",
                   DepartmentId = 1
               });

            
        }

    }
}
