using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ecomercewebapi.Models;
using ecomercewebapi.Data;
using ecomercewebapi.Dtos;
using MySql.Data.MySqlClient;
using static ecomercewebapi.Controllers.AdminloginController;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/*namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserloginController : ControllerBase
    {
       
        private readonly IUserService _userService; // Service for authentication
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";
        *//*  public UserloginController(ecomercewebapiContext context, IUserService userService)
          {
              _context = context;
              _userService = userService;
          }*//*

        // POST: api/UserloginDtoes/login
        *//* [HttpPost("login")]
         public async Task<IActionResult> Login([FromBody] UserloginDto userloginDto)
         {
             if (userloginDto == null || string.IsNullOrEmpty(userloginDto.Email) || string.IsNullOrEmpty(userloginDto.Password))
             {
                 return BadRequest("Invalid login attempt.");
             }

             var user = await _userService.AuthenticateUser(userloginDto.Email, userloginDto.Password);

             if (user == null)
             {
                 return Unauthorized("Invalid credentials.");
             }

             // Here, you can generate a token or any other authentication mechanism if needed.
             // For now, we'll return a simple success message.

             return Ok("Login successful.");
         }*//*

        // POST api/<AdminloginController>/login


       
          
            private readonly string _jwtSecretKey;
            private readonly int _jwtExpiryInMinutes = 60;

            public UserloginController(IUserService userService, IConfiguration configuration)
            {
                _userService = userService;
                _jwtSecretKey = configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret key not found.");
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] AdminLoginRequest request)
            {
                if (request == null)
                {
                    return BadRequest(new { message = "Login request data is null." });
                }

                if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
                {
                    return BadRequest(new { message = "Username and password cannot be empty." });
                }

                try
                {
                    using (MySqlConnection cn = new MySqlConnection(_connectionString))
                    {
                        cn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("SELECT Password FROM Users WHERE email = @username", cn))
                        {
                            cmd.Parameters.AddWithValue("@username", request.username);

                            var storedHashedPassword = cmd.ExecuteScalar() as string;

                            if (storedHashedPassword == null)
                            {
                                return Unauthorized(new { message = "Invalid username or password." });
                            }

                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.password, storedHashedPassword);

                            if (isPasswordValid)
                            {
                                var token = GenerateJwtToken(request.username);
                                return Ok(new { message = "Login successful.", token });
                            }
                            else
                            {
                                return Unauthorized(new { message = "Invalid username or password." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
                }
            }

            private string GenerateJwtToken(string username)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                        // Add more claims if needed, e.g. roles
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtExpiryInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
*/
namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserloginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";
        private readonly string _jwtSecretKey;
        private readonly int _jwtExpiryInMinutes = 60;

        public UserloginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtSecretKey = configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret key not found.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Login request data is null." });
            }

            if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest(new { message = "Username and password cannot be empty." });
            }

            try
            {
                using (MySqlConnection cn = new MySqlConnection(_connectionString))
                {
                    cn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT Password, Role FROM Users WHERE Email = @username", cn))
                    {
                        cmd.Parameters.AddWithValue("@username", request.username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var storedHashedPassword = reader["Password"].ToString();
                                var role = reader["Role"].ToString();

                                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.password, storedHashedPassword);

                                if (isPasswordValid)
                                {
                                    var token = GenerateJwtToken(request.username, role);
                                    return Ok(new { message = "Login successful.", token });
                                }
                            }
                        }

                        return Unauthorized(new { message = "Invalid username or password." });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}