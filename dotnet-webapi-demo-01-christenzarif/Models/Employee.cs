using dotnet_webapi_demo_01_christenzarif.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace dotnet_webapi_demo_01_christenzarif.Models
{

    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Miaximum 255 characters"), MinLength(3, ErrorMessage = "minimum 3 characters")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string UserName { get; private set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        [Range(100.00, 1000.00)]
        [Display(Name = "Monthly Salary")]
        public decimal Salary { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [ValidateAge]
        [Display(Name = "Birthdate")]
        [Required]
        public DateTime Age { get; set; }

        [Display(Name = "Create At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update At")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Department")]
        public Guid? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
