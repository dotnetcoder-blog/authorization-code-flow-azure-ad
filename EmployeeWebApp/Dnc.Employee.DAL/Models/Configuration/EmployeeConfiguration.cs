using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.DAL.Models.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "dbo");

            builder.Property(e => e.Salary)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ManagerId);

            builder.HasMany(e => e.Projects)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

    //ClientSetNull Sets foreign key values to null as appropriate when changes are made to tracked entities,
    //and creates a non-cascading foreign key constraint in the database. This is the default for optional relationships.
}
