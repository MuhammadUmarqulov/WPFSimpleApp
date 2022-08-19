using ExamTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ExamTask.Data.DbContexts
{
    public class UniversityDbContext : DbContext
    {
        public virtual DbSet<User> Students { get; set; }
        public virtual DbSet<BaseAttachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host = localhost; Port = 5432; Database = universityDb; Username = postgres; Password = root");
        }


    }
}
