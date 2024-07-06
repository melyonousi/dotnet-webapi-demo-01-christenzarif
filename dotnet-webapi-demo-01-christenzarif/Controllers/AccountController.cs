using dotnet_webapi_demo_01_christenzarif.DTO;
using dotnet_webapi_demo_01_christenzarif.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_webapi_demo_01_christenzarif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDTO>> Create(UserRegisterDTO userDTO)
        {
            //var modelState = new ModelStateDictionary();
            //ValideUser(user, modelState);

            try
            {
                if (ModelState.IsValid)
                {
                    User user = new()
                    {
                        UserName = userDTO.UserName,
                        Email = userDTO.Email,
                        PhoneNumber = userDTO.PhoneNumber,
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
                    if (result.Succeeded)
                    {
                        return Created("/login", result);
                    }
                    return BadRequest(result.Errors);
                }

                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return Conflict(new
                {
                    code = "DuplicatePhoneNumber",
                    description = $"Phone Number is already taken."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while creating the user.\n {ex}" });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user is not null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (found)
                    {
                        //Claims Token
                        var claims = new List<Claim>
                        {
                            new (ClaimTypes.Name, user.UserName!),
                            new (ClaimTypes.NameIdentifier, user.Id!),
                            new (ClaimTypes.Email, user.Email!),
                            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        //get role
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Any())
                        {
                            foreach (string item in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, item));
                            }
                        }

                        //Security Key
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:signin_key"]!));
                        // signin credentials
                        SigningCredentials signing = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256);

                        //Create token
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _configuration["JWT:issuer"], //provider: backend url (web api, current)
                            audience: _configuration["JWT:audience"], // consumer: front end
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signing
                        );

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        private void ValideUser(UserRegisterDTO registerDTO, ModelStateDictionary state)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                state.AddModelError("Password", "The Name field is required.");
            }
        }
    }
}
