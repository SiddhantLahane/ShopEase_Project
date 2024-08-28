

namespace ecomercewebapi.Controllers
{
    using ecomercewebapi.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ShipperDTOesController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

        // GET: api/ShipperDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDTO>>> GetShipperDTO()
        {
            var shippers = new List<ShipperDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ContactNumber, Email FROM Shipper";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        shippers.Add(new ShipperDTO
                        {
                            Id = reader.GetInt32("Id"),  // Use GetInt32 if Id is an int in the database
                            Name = reader.GetString("Name"),
                            ContactNumber = reader.GetString("ContactNumber"),
                            Email = reader.IsDBNull("Email") ? null : reader.GetString("Email")
                        });
                    }
                }
            }

            return Ok(shippers);
        }

        // GET: api/ShipperDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipperDTO>> GetShipperDTO(int id)  // Change parameter type to int
        {
            ShipperDTO shipperDTO = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, ContactNumber, Email FROM Shipper WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            shipperDTO = new ShipperDTO
                            {
                                Id = reader.GetInt32("Id"),  // Use GetInt32 if Id is an int in the database
                                Name = reader.GetString("Name"),
                                ContactNumber = reader.GetString("ContactNumber"),
                                Email = reader.IsDBNull("Email") ? null : reader.GetString("Email")
                            };
                        }
                    }
                }
            }

            if (shipperDTO == null)
            {
                return NotFound();
            }

            return Ok(shipperDTO);
        }

        // PUT: api/ShipperDTOes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipperDTO(int id, ShipperDTO shipperDTO)  // Change parameter type to int
        {
            /*if (id != shipperDTO.Id)
            {
                return BadRequest();
            }*/

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Shipper 
                      SET Name = @Name, ContactNumber = @ContactNumber, Email = @Email
                      WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", shipperDTO.Id);
                    command.Parameters.AddWithValue("@Name", shipperDTO.Name);
                    command.Parameters.AddWithValue("@ContactNumber", shipperDTO.ContactNumber);
                    command.Parameters.AddWithValue("@Email", shipperDTO.Email ?? (object)DBNull.Value);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }

        // POST: api/ShipperDTOes
        [HttpPost]
        public async Task<ActionResult> PostShipperDTO(ShipperDTO shipperDTO)
        {
            if (shipperDTO == null)
            {
                return BadRequest(new { message = "ShipperDTO data is null." });
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Shipper (Name, ContactNumber, Email) 
                      VALUES (@Name, @ContactNumber, @Email)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", shipperDTO.Name);
                    command.Parameters.AddWithValue("@ContactNumber", shipperDTO.ContactNumber);
                    command.Parameters.AddWithValue("@Email", shipperDTO.Email ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { message = "ShipperDTO created successfully." });
        }

        // DELETE: api/ShipperDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipperDTO(int id)  // Change parameter type to int
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Shipper WHERE Id = @Id";

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
}
