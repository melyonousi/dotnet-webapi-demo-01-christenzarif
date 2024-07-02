namespace dotnet_webapi_demo_01_christenzarif.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
        public virtual List<Employee>? Employees { get; set; }
    }
}
