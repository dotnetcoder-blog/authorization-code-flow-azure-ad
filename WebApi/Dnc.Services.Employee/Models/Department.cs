using Dnc.Services.Employee.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Employee.Models
{
    [EntityTypeConfiguration(typeof(DepartmentConfiguration))]
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();


    }
}
