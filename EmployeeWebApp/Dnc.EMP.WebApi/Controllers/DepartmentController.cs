using Dnc.EMP.DAL.Models;
using Dnc.EMP.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dnc.EMP.WebApi.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        // GET: api/department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await departmentService.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpGet("{id}/NoTracking")]
        public async Task<ActionResult<Department>> GetDepartmentAsNoTracking(int id)
        {
            var department = await departmentService.GetAsNoTrackingAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        // GET: api/department
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await departmentService.GetAllAsync();
            if (departments == null)
            {
                return NotFound();
            }

            return new ActionResult<IEnumerable<Department>>(departments);
        }

        [HttpGet("all/IgnoreQueryFilters")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartmentsIgnoreQueryFilters()
        {
            var departments = await departmentService.GetAllIgnoreQueryFiltersAsync();
            if (departments == null)
            {
                return NotFound();
            }

            return new ActionResult<IEnumerable<Department>>(departments);
        }
    }
}
