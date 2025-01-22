using System;
using Microsoft.EntityFrameworkCore;
namespace q3
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "John", Grade = 5 },
                new Student { Id = 2, Name = "Get", Grade = 11 }
                );
        }

    }
}
