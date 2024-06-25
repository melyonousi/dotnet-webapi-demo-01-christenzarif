using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_demo_01_christenzarif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Employee()
        {
            var employees = await _employee.Get();
            return employees is not null ? Ok(employees) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Employee(Guid id)
        {
            var employee = await _employee.Get(id);
            return employee is not null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Employee(Employee employee)
        {
            var emp = await _employee.Create(employee);
            return emp is not null ? Ok(emp) : NotFound();
        }
    }
}
