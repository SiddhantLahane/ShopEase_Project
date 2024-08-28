using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SubCategoryController : ControllerBase
{
    private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

    // GET: api/SubCategory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetSubCategories()
    {
        var subCategories = new List<SubCategoryDTO>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Name, CategoryId FROM SubCategory";

            using (var command = new MySqlCommand(query, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    subCategories.Add(new SubCategoryDTO
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        CategoryId = reader.GetInt64("CategoryId")
                    });
                }
            }
        }

        return Ok(subCategories);
    }

    // GET: api/SubCategory/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategoryDTO>> GetSubCategory(long id)
    {
        SubCategoryDTO subCategoryDTO = null;

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Id, Name, CategoryId FROM SubCategory WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        subCategoryDTO = new SubCategoryDTO
                        {
                            Id = reader.GetInt64("Id"),
                            Name = reader.GetString("Name"),
                            CategoryId = reader.GetInt64("CategoryId")
                        };
                    }
                }
            }
        }

        if (subCategoryDTO == null)
        {
            return NotFound();
        }

        return Ok(subCategoryDTO);
    }

    // PUT: api/SubCategory/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubCategory(long id, SubCategoryDTO subCategoryDTO)
    {
        if (id != subCategoryDTO.Id)
        {
            return BadRequest();
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"UPDATE SubCategory 
                              SET Name = @Name, CategoryId = @CategoryId
                              WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", subCategoryDTO.Id);
                command.Parameters.AddWithValue("@Name", subCategoryDTO.Name);
                command.Parameters.AddWithValue("@CategoryId", subCategoryDTO.CategoryId);

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }

    // POST: api/SubCategory
    [HttpPost]
    public async Task<ActionResult> PostSubCategory(SubCategoryDTO subCategoryDTO)
    {
        if (subCategoryDTO == null)
        {
            return BadRequest(new { message = "SubCategoryDTO data is null." });
        }

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"INSERT INTO SubCategory (Name, CategoryId) 
                              VALUES (@Name, @CategoryId)";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", subCategoryDTO.Name);
                command.Parameters.AddWithValue("@CategoryId", subCategoryDTO.CategoryId);

                await command.ExecuteNonQueryAsync();
            }
        }

        return Ok(new { message = "SubCategoryDTO created successfully." });
    }

    // DELETE: api/SubCategory/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategory(long id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM SubCategory WHERE Id = @Id";

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

public class SubCategoryDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long CategoryId { get; set; }
}
