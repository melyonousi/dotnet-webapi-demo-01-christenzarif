using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_webapi_demo_01_christenzarif.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Salary { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateOnly Age { get; set; }
    }
}
