using dotnet_webapi_demo_01_christenzarif.Models;
using System.ComponentModel;

namespace dotnet_webapi_demo_01_christenzarif.DTO
{
    public class DepartmentWithEmployeesDTO
    {
        [DisplayName("Department ID")]
        public Guid Id { get; set; }

        [DisplayName("Department Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Employees")]
        public List<string>? EmployeesName { get; set; }
    }

    public class DepartmentWithEmployeesNameDTO
    {
        [DisplayName("Department ID")]
        public Guid Id { get; set; }

        [DisplayName("Department Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Employees Name")]
        public List<EmployeeNameDTO> Employees { get; set; } = new List<EmployeeNameDTO>();
    }

    public class DepartmentNameDTO
    {
        [DisplayName("Department ID")]
        public Guid Id { get; set; }

        [DisplayName("Department Name")]
        public string Name { get; set; } = string.Empty;
    }
}
