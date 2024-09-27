using Dnc.Services.Employee.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Employee.Models
{
    //The final step for the Employee class is to add the EntityTypeConfiguration attribute:
    [EntityTypeConfiguration(typeof(EmployeeConfiguration))]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public Decimal Salary { get; set; }

        public Employee? Manager { get; set; } //his overrides the nullability of the value null to non-nullable, telling the compiler that null is a "non-null" type.
        public int? ManagerId { get; set; }

        public Department Department { get; set; } = null!;
        public int DepartmentId { get; set; }

        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
    }
}
