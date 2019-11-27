using Microsoft.EntityFrameworkCore;

namespace TestEfcore.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions opt)
            : base(opt)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
