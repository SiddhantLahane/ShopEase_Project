using ecomercewebapi.Dtos;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SellersController : ControllerBase
{
    private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

    // GET: api/Sellers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SellerDTO>>> GetSellers()
    {
        var sellers = new List<SellerDTO>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Name, ContactNumber, Email, Address FROM Sellers";

            using (var command = new MySqlCommand(query, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    sellers.Add(new SellerDTO
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        ContactNumber = reader.GetString("ContactNumber"),
                        Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                        Address = reader.IsDBNull("Address") ? null : reader.GetString("Address")
                    });
                }
            }
        }

        return Ok(sellers);
    }

    // GET: api/Sellers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SellerDTO>> GetSeller(long id)
    {
        SellerDTO sellerDTO = null;

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Name, ContactNumber, Email, Address FROM Sellers WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sellerDTO = new SellerDTO
                        {
                            Id = reader.GetInt64("Id"),
                            Name = reader.GetString("Name"),
                            ContactNumber = reader.GetString("ContactNumber"),
                            Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                            Address = reader.IsDBNull("Address") ? null : reader.GetString("Address")
                        };
                    }
                }
            }
        }

        if (sellerDTO == null)
        {
            return NotFound();
        }

        return Ok(sellerDTO);
    }

    // PUT: api/Sellers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeller(long id, SellerDTO sellerDTO)
    {
        if (id != sellerDTO.Id)
        {
            return BadRequest();
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"UPDATE Sellers 
                             SET Name = @Name, ContactNumber = @ContactNumber, Email = @Email, Address = @Address
                             WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", sellerDTO.Id);
                command.Parameters.AddWithValue("@Name", sellerDTO.Name);
                command.Parameters.AddWithValue("@ContactNumber", sellerDTO.ContactNumber);
                command.Parameters.AddWithValue("@Email", sellerDTO.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", sellerDTO.Address ?? (object)DBNull.Value);

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }

    // POST: api/Sellers
    [HttpPost]
    public async Task<ActionResult> PostSeller(SellerDTO sellerDTO)
    {
        if (sellerDTO == null)
        {
            return BadRequest(new { message = "SellerDTO data is null." });
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"INSERT INTO Sellers (Name, ContactNumber, Email, Address) 
                             VALUES (@Name, @ContactNumber, @Email, @Address)";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", sellerDTO.Name);
                command.Parameters.AddWithValue("@ContactNumber", sellerDTO.ContactNumber);
                command.Parameters.AddWithValue("@Email", sellerDTO.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", sellerDTO.Address ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        return Ok(new { message = "SellerDTO created successfully." });
    }

    // DELETE: api/Sellers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeller(long id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Sellers WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }
}
