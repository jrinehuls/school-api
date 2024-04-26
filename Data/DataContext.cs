using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data.Config;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }

    }
}
