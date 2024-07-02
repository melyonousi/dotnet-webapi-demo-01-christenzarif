using System.ComponentModel;
using System.Text.Json.Serialization;

namespace dotnet_webapi_demo_01_christenzarif.DTO
{
    public class EmployeeWithDepartmentNameDTO
    {
        [DisplayName("Employee ID")]
        public Guid Id { get; set; }

        [DisplayName("Employee Name")]
        public string Name{ get; set; } = string.Empty;

        [DisplayName("Department Name")]
        [JsonPropertyName("department_name")]
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class EmployeeNameDTO
    {
        [DisplayName("Employee ID")]
        public Guid Id { get; set; }

        [DisplayName("Employee Name")]
        public string Name { get; set; } = string.Empty;
    }
}
