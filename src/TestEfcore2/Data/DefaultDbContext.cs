using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TestEfcore.Data
{
   
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions opt)
            : base(opt)
        {
            //IsDeleted = isDeleted;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public bool IsDeleted { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
