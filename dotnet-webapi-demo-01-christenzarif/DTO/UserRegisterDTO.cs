using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_webapi_demo_01_christenzarif.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(7)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
