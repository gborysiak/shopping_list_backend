using dotnet_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly string secretkey;
        IConfiguration configuration;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

            this.secretkey = this.configuration.GetValue<string>("SecretKey");
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] UserVM user)
        {
            User newuser = new User();

            if (!ModelState.IsValid)
                return BadRequest("Données invalides");

            newuser.UserName = user.username;
            newuser.Email =user.email;
            newuser.name = user.name;
            newuser.createdAt = DateTime.Now;

            var result = await this.userManager.CreateAsync(newuser, user.Password);

            if (result.Succeeded)
                return Ok();

            return BadRequest("Erreur dans la création de l'utilisateur : " + string.Join(",", result.Errors.Select(error => error.Description)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Données invalides");

            var result = await this.signInManager.PasswordSignInAsync(
                user.Username,
                user.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
                return Ok(new LoginResponseVM
                {
                    Username = user.Username,
                    Password = user.Password,
                    Token = this.GenerateToken(user.Username)
                });

            return BadRequest("Erreur dans l'authentification de l'utilisateur");
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
