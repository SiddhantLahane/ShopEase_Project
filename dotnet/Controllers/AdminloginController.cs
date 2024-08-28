using ecomercewebapi.Dtos;
using ecomercewebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient; // Use MySQL connector

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminloginController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

        // POST api/<AdminloginController>/login
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

                    using (MySqlCommand cmd = new MySqlCommand("SELECT Password FROM Admins WHERE username = @username", cn))
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
                            return Ok("Login successful." );
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

        // POST api/<AdminloginController>/create
        [HttpPost("create")]
        public IActionResult CreateAdmin([FromBody] AdminCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Admin creation request data is null." });
            }

            if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest(new { message = "Username and password cannot be empty." });
            }

            // Hash the password before storing
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

            try
            {
                using (MySqlConnection cn = new MySqlConnection(_connectionString))
                {
                    cn.Open();

                    string query = "INSERT INTO Admins (username, Password) VALUES (@username, @password)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@username", request.username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { message = "Admin created successfully." });
                        }
                        else
                        {
                            return StatusCode(500, new { message = "Failed to create admin. Please try again." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        // Define models used for requests
        public class AdminLoginRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class AdminCreateRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }



}
