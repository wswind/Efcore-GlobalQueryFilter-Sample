using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TestEfcore.Data
{
    public class DefaultDbContext : DbContext
    {
        private readonly Tenant _tenant;

        //public DefaultDbContext(ITenantProvider tenantProvider)
        //{
        //    _tenant = tenantProvider.GetTenant();
        //}
        public DefaultDbContext(DbContextOptions opt, ITenantProvider tenantProvider)
            : base(opt)
        {
            //IsDeleted = isDeleted;
            _tenant = tenantProvider.GetTenant();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public bool IsDeleted { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach(var entityType in entityTypes)
            {
                var clrType = entityType.ClrType;
                if (clrType.GetInterfaces().Any(x=>x == typeof(ISoftDelete)))
                {
                    var parameter = Expression.Parameter(clrType);
                    // EF.Property<bool>(post, "IsDeleted")
                    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                    var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
                    // EF.Property<bool>(post, "IsDeleted") == false
                    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                    // post => EF.Property<bool>(post, "IsDeleted") == false
                    var lambda = Expression.Lambda(compareExpression, parameter);
                    modelBuilder.Entity(clrType).HasQueryFilter(lambda);
                }

            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
