using Dnc.EMP.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.DAL.Models
{
    [EntityTypeConfiguration(typeof(DepartmentConfiguration))]
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; } 

        public IEnumerable<Employee> Employees { get; set; }
    }
}
