using Dnc.EMP.DAL.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.DAL.Models
{
    [EntityTypeConfiguration(typeof(ProjectConfiguration))]
    public class Project : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
