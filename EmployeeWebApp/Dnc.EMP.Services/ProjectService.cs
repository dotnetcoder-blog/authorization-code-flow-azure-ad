using Dnc.EMP.DAL;
using Dnc.EMP.DAL.Models;
using Dnc.EMP.Services.Abstract;
using Dnc.EMP.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.EMP.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        public ProjectService(AppDbContext context) : base(context)
        {
        }
    }
}
