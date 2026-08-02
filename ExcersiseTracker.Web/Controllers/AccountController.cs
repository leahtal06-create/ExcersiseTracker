using ExcersiseTracker.Data;
using ExcersiseTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExcersiseTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private string _connectionString;
        private IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _configuration = configuration;
        }

        [HttpPost]
        [Route("signup")]
        public void Signup(SignupViewModel user)
        {
            var repo = new UserRepository(_connectionString);
            repo.AddUser(user, user.Password);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel viewModel)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            // validate secret exists
            var secret = _configuration.GetValue<string>("JWTSecret");
            if (string.IsNullOrWhiteSpace(secret))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Server not configured for authentication." });
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, viewModel.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // include expiry
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { token = tokenString });
        }

        [HttpGet]
        [Route("getcurrentuser")]
        [Authorize]
        public User GetCurrentUser()
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (String.IsNullOrEmpty(email))
            {
                return null;
            }

            var repo = new UserRepository(_connectionString);
            return repo.GetByEmail(email);
        }
    }
}