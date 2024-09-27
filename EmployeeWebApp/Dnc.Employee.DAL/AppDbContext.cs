using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnc.EMP.DAL.Models;
using Dnc.EMP.DAL.Models.Configuration;

namespace Dnc.EMP.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Project> Projects => Set<Project>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //The base DbContext class exposes the OnModelCreating method that is used to shape your entities using the Fluent API.

        //Replace all the code in the OnModelBuilding() method (in the ApplicationDbContext.cs class) that
        //configures the Car class and the Car to Driver many-to-many relationship with the following single line
        // of code:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EmployeeConfiguration().Configure(modelBuilder.Entity<Models.Employee>());
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
            new ProjectConfiguration().Configure(modelBuilder.Entity<Project>());
        }
    }
}
