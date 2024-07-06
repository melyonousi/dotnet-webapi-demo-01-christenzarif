using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace dotnet_webapi_demo_01_christenzarif.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
