using System.Threading.Tasks;
using ecomercewebapi.Data;
using ecomercewebapi.Models;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ecomercewebapiContext _context;

    public UserService(ecomercewebapiContext context)
    {
        _context = context;
    }

    public async Task<Users> AuthenticateUser(string username, string password)
    {
        // Retrieve the user based on the email (or username)
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == username);

        // Check if the user exists and if the password matches
        if (user == null || user.Password != password) // Plain text password comparison
        {
            return null; // Invalid credentials
        }

        return user;
    }
}
