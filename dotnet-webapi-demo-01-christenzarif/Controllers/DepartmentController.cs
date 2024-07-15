using dotnet_webapi_demo_01_christenzarif.DTO;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_demo_01_christenzarif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _department;
        private readonly IConfiguration configuration;

        public DepartmentController(IDepartment department, IConfiguration configuration)
        {
            _department = department;
            this.configuration = configuration;
        }

        [HttpGet("myvar")]
        public IActionResult GetVariable()
        {
            return Ok(new
            {
                fromRenderVar = Environment.GetEnvironmentVariable("myAppVar"),
                fromRenderVarSettings = configuration["myAppVar"]
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentNameDTO>>> GetDepartmentsWithName()
        {
            List<DepartmentNameDTO> nameDTO = await _department.GetDepartment();
            if (nameDTO is not null)
            {
                return Ok(nameDTO);
            }
            return NotFound();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartmentWithEmployeesNameDTO>> GetDepartmentWithEmployees(Guid id)
        {
            DepartmentWithEmployeesNameDTO nameDTO = await _department.GetDepartment(id);
            if (nameDTO is not null)
            {
                return Ok(nameDTO);
            }
            return NotFound();
        }
    }
}
