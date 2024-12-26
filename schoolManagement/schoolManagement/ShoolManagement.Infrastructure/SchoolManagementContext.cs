using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace ShoolManagement.Infrastructure
{
    public class SchoolManagementContext : DbContext
    {
        public SchoolManagementContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Student and StudentAddress
            modelBuilder.Entity<StudentAddress>()
                .HasOne(sa => sa.Student) // Each StudentAddress is associated with one Student
                .WithMany(s => s.Addresses) // Each Student can have many StudentAddresses
                .HasForeignKey(sa => sa.StudentId) // Specify StudentId as the foreign key
                .IsRequired(); // Ensure the relationship is required (StudentId cannot be null)
        }


    }
}
