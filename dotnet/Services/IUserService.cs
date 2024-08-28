using ecomercewebapi.Migrations;
using ecomercewebapi.Models;

public interface IUserService
{
    Task<Users> AuthenticateUser(string username, string password);
}
