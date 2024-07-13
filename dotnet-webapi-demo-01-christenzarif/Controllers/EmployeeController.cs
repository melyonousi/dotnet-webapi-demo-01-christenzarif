using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using dotnet_webapi_demo_01_christenzarif.DTO;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_webapi_demo_01_christenzarif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly IConfiguration configuration;

        public EmployeeController(IEmployee employee, IConfiguration configuration)
        {
            _employee = employee;
            this.configuration = configuration;
        }

        [HttpGet("myvar")]
        public IActionResult GetVariable()
        {
            return Ok(new
            {
                fromRenderVar = Environment.GetEnvironmentVariable("myAppVar"),
                fromRenderVarSettings = configuration["App:myAppVar"]
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _employee.Get();
            return employees is not null ? Ok(employees) : NoContent();
        }

        [HttpGet("{id:guid:required}", Name = "EmployeeDetailRoute")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _employee.Get(id);
            return employee is not null ? Ok(employee) : NotFound($"employee with id: \"{id}\" not found");
        }

        [HttpGet("{username:alpha:required}")]
        public async Task<ActionResult<Employee>> GetEmployee(string username)
        {
            var employee = await _employee.Get(username);
            return employee is not null ? Ok(employee) : NotFound($"employee with username: \"{username}\" not found");
        }

        [HttpDelete("{id:guid:required}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(Guid id)
        {
            var employee = await _employee.Delete(id);
            return employee is not false ? StatusCode(StatusCodes.Status204NoContent, employee) : NotFound($"employee with id: \"{id}\" not found");
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee emp = await _employee.Create(employee);
                return emp is not null ? Created($"{Url.Link("EmployeeDetailRoute", new { id = emp.Id })}", emp) : BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = await _employee.Update(employee);
                return emp is not null ? Ok(emp) : NotFound($"employee with id: \"{employee.Id}\" not found");
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id:guid:required}/department")]
        public async Task<ActionResult<EmployeeWithDepartmentNameDTO>> GetEmployeeByIdWithDepartment(Guid id)
        {
            var employee = await _employee.GetEmployeeByIdWithDepartmentName(id);
            return employee is not null ? Ok(employee) : NotFound($"employee with id: \"{id}\" not found");
        }
    }
}
