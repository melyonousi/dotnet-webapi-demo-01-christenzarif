using dotnet_webapi_demo_01_christenzarif.DTO;
using dotnet_webapi_demo_01_christenzarif.Models;
using dotnet_webapi_demo_01_christenzarif.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_demo_01_christenzarif.Repositories.Implement
{
    public class DepartmentRepository : IDepartment
    {
        private readonly DataContext _dataContext;

        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<DepartmentWithEmployeesNameDTO> GetDepartment(Guid guid)
        {
            Department? department = await _dataContext.Departments.Include(opt => opt.Employees).FirstOrDefaultAsync();
            if (department == null)
            {
                return null!;
            }

            DepartmentWithEmployeesNameDTO nameDTO = new DepartmentWithEmployeesNameDTO
            {
                Id = department!.Id,
                Name = department.Name,
                Employees = department.Employees!.Select(opt => new EmployeeNameDTO
                {
                    Id = opt.Id,
                    Name = opt.Name,
                }).ToList()
            };

            return nameDTO;
        }

        public async Task<List<DepartmentNameDTO>> GetDepartment()
        {
            List<DepartmentNameDTO> dpts = await _dataContext.Departments.Select(opt => new DepartmentNameDTO
            {
                Id = opt.Id,
                Name = opt.Name,
            }).ToListAsync();

            return dpts is not null ? dpts : null!;
        }
    }
}
