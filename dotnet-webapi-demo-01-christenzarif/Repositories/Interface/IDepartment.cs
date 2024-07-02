using dotnet_webapi_demo_01_christenzarif.DTO;
using dotnet_webapi_demo_01_christenzarif.Models;

namespace dotnet_webapi_demo_01_christenzarif.Repositories.Interface
{
    public interface IDepartment
    {
        public Task<List<DepartmentNameDTO>> GetDepartment();
        public Task<DepartmentWithEmployeesNameDTO> GetDepartment(Guid guid);
    }
}
