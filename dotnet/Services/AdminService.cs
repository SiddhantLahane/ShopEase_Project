using ecomercewebapi.Dtos;
using ecomercewebapi.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ecomercewebapi.Data;

namespace ecomercewebapi.Services
{
    public class AdminService
    {
        private readonly ecomercewebapiContext _context;

        public AdminService(ecomercewebapiContext context)
        {
            _context = context;
        }

        // Method to register an admin
        public async Task RegisterAdmin(AdminLoginDto adminDto)
        {
            // Create a new Admin entity from the provided DTO
            var admin = new Admin
            {
               // id = adminDto.Id, // Ensure 'Id' matches your Admin model property name
              // name = adminDto.Name, // Ensure 'Name' matches your Admin model property name
                username = adminDto.username, // Ensure 'UserName' matches your Admin model property name
                password = BCrypt.Net.BCrypt.HashPassword(adminDto.password) // Hash the password
            };

            // Add the admin to the database context
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();
        }

        // Method to authenticate an admin
        public async Task<Admin> AuthenticateAdmin(string username, string password)
        {
            // Retrieve the admin based on the username
            var admin = await _context.Admin.SingleOrDefaultAsync(a => a.username == username);

            // Verify the provided password against the stored hash
            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.password))
            {
                return null; // Authentication failed
            }

            return admin; // Authentication successful
        }
    }
}
