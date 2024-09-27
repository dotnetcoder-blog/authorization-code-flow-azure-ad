using Dnc.Services.Employee.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Employee.Models
{
    [EntityTypeConfiguration(typeof(ProjectConfiguration))]
    public class Project
    {
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; } = null!;
        public int EmployeeId { get; set; }

    }
}
