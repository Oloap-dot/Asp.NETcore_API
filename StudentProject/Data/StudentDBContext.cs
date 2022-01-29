using Microsoft.EntityFrameworkCore;
using Students.Models;

namespace Students.Data
{
    public class StudentDBContext : DbContext
    {
        //we'll setup the database from the Program.cs (i also pass options to the inherited class constructor)
        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options) {}

        //i setup a table of Students
        public DbSet<Student> Students {get; set;}

    }
}