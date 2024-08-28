using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ecomercewebapi.Models;
using ecomercewebapi.Migrations;
using MySql.Data.MySqlClient;
using ecomercewebapi.Data;

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       /* private readonly ecomercewebapiContext _context;

        public UsersController(ecomercewebapiContext context)
        {
            _context = context;
        }*/

        // GET: api/Users
        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
         {
             return await _context.Users.ToListAsync();
         }

         // GET: api/Users/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Users>> GetUsers(int id)
         {
             var users = await _context.Users.FindAsync(id);

             if (users == null)
             {
                 return NotFound();
             }

             return users;
         }*/

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*  [HttpPut("{id}")]
          public async Task<IActionResult> PutUsers(int id, Users users)
          {
              if (id != users.Id)
              {
                  return BadRequest();
              }

              _context.Entry(users).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!UsersExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
          }*/

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }*/

        /*        [HttpPost]
                public async Task<ActionResult> InsertUserAsync(Users user)
                {
                    if (user == null)
                    {
                        return BadRequest("User data is null.");
                    }

                    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                    {
                        return BadRequest("Email and password are required.");
                    }

                    string connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

                    using (var connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            await connection.OpenAsync();

                            string query = @"INSERT INTO Users (Name, Gender, Mobile, Address, City, Pincode, Email, Password) 
                                     VALUES (@Name, @Gender, @Mobile, @Address, @City, @Pincode, @Email, @Password);";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Name", user.Name);
                                command.Parameters.AddWithValue("@Gender", user.Gender);
                                command.Parameters.AddWithValue("@Mobile", user.Mobile);
                                command.Parameters.AddWithValue("@Address", user.Address);
                                command.Parameters.AddWithValue("@City", user.City);
                                command.Parameters.AddWithValue("@Pincode", user.Pincode);
                                command.Parameters.AddWithValue("@Email", user.Email);
                                command.Parameters.AddWithValue("@Password", user.Password); // Ensure password is hashed

                                await command.ExecuteNonQueryAsync();
                            }

                            return Ok("User created successfully.");
                        }
                        catch (Exception ex)
                        {
                            // Handle exception
                            return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
                        }
                    }
                }*/

        [HttpPost]
        public async Task<ActionResult> InsertUserAsync(Users user)
        {
            if (user == null)
            {
                return BadRequest(new { message = "User data is null." });
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            string connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"INSERT INTO Users (Name, Gender, Mobile, Address, City, Pincode, Email, Password) 
            VALUES (@Name, @Gender, @Mobile, @Address, @City, @Pincode, @Email, @Password);";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Gender", user.Gender);
                        command.Parameters.AddWithValue("@Mobile", user.Mobile);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@City", user.City);
                        command.Parameters.AddWithValue("@Pincode", user.Pincode);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Use hashed password

                        await command.ExecuteNonQueryAsync();
                    }

                    return Ok(new { message = "User created successfully." });
                }
                catch (Exception ex)
                {
                    // Enhanced logging
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                    return StatusCode(500, new { message = "Internal server error: Please check the server logs." });
                }
            }
        }
        // DELETE: api/Users/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }*/
    }
}
