using Dnc.EMP.DAL.Models;
using Dnc.EMP.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Xml.Linq;

namespace Dnc.EMP.WebApi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet("{id}/NoTracking")]
        public async Task<ActionResult<Employee>> GetEmployeeAsNoTracking(int id)
        {
            var employee = await employeeService.GetAsNoTrackingAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // GET: api/employee
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await employeeService.GetAllAsync();
            if (employees == null)
            {
                return NotFound();
            }

            return new ActionResult<IEnumerable<Employee>>(employees);
        }

        [HttpGet("all/IgnoreQueryFilters")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesIgnoreQueryFilters()
        {
            var employees = await employeeService.GetAllIgnoreQueryFiltersAsync();
            if (employees == null)
            {
                return NotFound();
            }

            return new ActionResult<IEnumerable<Employee>>(employees);
        }

        [HttpGet("{salary}/sql")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetByExecutingSqlString(decimal salary)
        {
            var employees = await employeeService.ExecuteSqlStringAsync($"select * from Employees where Salary > {salary}");
            if (employees == null)
            {
                return NotFound();
            }

            return new ActionResult<IEnumerable<Employee>>(employees);
        }

        //[HttpGet("{sql}/ParameterizedQuery")]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetByExecutingParameterizedQuery(decimal salary)
        //{
        //    var employees = await employeeService.ExecuteSqlStringAsync($"select * from Employees where Salary > {salary}");
        //    if (employees == null)
        //    {
        //        return NotFound();
        //    }

        //    return new ActionResult<IEnumerable<Employee>>(employees);
        //}

        // POST: api/employee
        [HttpPost]
        public async Task<int> CreateEmployee(Employee employee)
        {
            return await employeeService.AddAsync(employee);
        }

        [HttpPost("range")]
        public async Task<int> CreateEmployees(IEnumerable<Employee> employees)
        {
            return await employeeService.AddRangeAsync(employees);
        }

        [HttpPut]
        public async Task<int> UpdateEmployee(Employee employee)
        {
            return await employeeService.UpdateAsync(employee);
        }

        [HttpPut("range")]
        public async Task<int> UpdateEmployees(IEnumerable<Employee> employees)
        {
            return await employeeService.UpdateRangeAsync(employees);
        }

        [HttpDelete]
        public async Task<int> DeleteEmployee(Employee employee)
        {
            return await employeeService.DeleteAsync(employee);
        }

        [HttpDelete("range")]
        public async Task<int> DeleteEmployees(IEnumerable<Employee> employees)
        {
            return await employeeService.DeleteRangeAsync(employees);
        }
    }
}
