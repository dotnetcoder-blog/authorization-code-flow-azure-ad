using Dnc.EMP.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.DAL.Models
{
    [EntityTypeConfiguration(typeof(EmployeeConfiguration))]
    public class Employee :BaseEntity
    {
        public string Name { get; set; } 
        public string Job { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public Employee Manager { get; set; }
        public int? ManagerId { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
