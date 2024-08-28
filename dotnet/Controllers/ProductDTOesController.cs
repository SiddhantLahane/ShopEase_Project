using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomercewebapi.Data;
using ecomercewebapi.Dtos;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDTOesController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

        [Authorize]
        // GET: api/ProductDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductDTO()
        {
            var products = new List<ProductDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description, Price, StockQuantity, CategoryId, SubCategoryId, ImageUrl, CreatedDate, ModifiedDate FROM Products";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new ProductDTO
                        {
                            Id = reader.GetInt64("Id"),  // Use GetInt64 if Id is a long in the database
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description"),
                            Price = reader.GetDecimal("Price"),
                            StockQuantity = reader.GetInt32("StockQuantity"),
                            CategoryId = reader.GetInt64("CategoryId"),
                            SubCategoryId = reader.IsDBNull("SubCategoryId") ? (long?)null : reader.GetInt64("SubCategoryId"),
                            ImageUrl = reader.GetString("ImageUrl"),
                            CreatedDate = reader.GetDateTime("CreatedDate"),
                            ModifiedDate = reader.IsDBNull("ModifiedDate") ? (DateTime?)null : reader.GetDateTime("ModifiedDate")
                        });
                    }
                }
            }

            return Ok(products);
        }

        // GET: api/ProductDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductDTO(long id)  // Change parameter type to long
        {
            ProductDTO productDTO = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Name, Description, Price, StockQuantity, CategoryId, SubCategoryId, ImageUrl, CreatedDate, ModifiedDate FROM Products WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            productDTO = new ProductDTO
                            {
                                Id = reader.GetInt64("Id"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description"),
                                Price = reader.GetDecimal("Price"),
                                StockQuantity = reader.GetInt32("StockQuantity"),
                                CategoryId = reader.GetInt64("CategoryId"),
                                SubCategoryId = reader.IsDBNull("SubCategoryId") ? (long?)null : reader.GetInt64("SubCategoryId"),
                                ImageUrl = reader.GetString("ImageUrl"),
                                CreatedDate = reader.GetDateTime("CreatedDate"),
                                ModifiedDate = reader.IsDBNull("ModifiedDate") ? (DateTime?)null : reader.GetDateTime("ModifiedDate")
                            };
                        }
                    }
                }
            }

            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }

        // PUT: api/ProductDTOes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDTO(long id, ProductDTO productDTO)  // Change parameter type to long
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Products 
                  SET Name = @Name, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, 
                      CategoryId = @CategoryId, SubCategoryId = @SubCategoryId, ImageUrl = @ImageUrl,
                      CreatedDate = @CreatedDate, ModifiedDate = @ModifiedDate
                  WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", productDTO.Id);
                    command.Parameters.AddWithValue("@Name", productDTO.Name);
                    command.Parameters.AddWithValue("@Description", productDTO.Description);
                    command.Parameters.AddWithValue("@Price", productDTO.Price);
                    command.Parameters.AddWithValue("@StockQuantity", productDTO.StockQuantity);
                    command.Parameters.AddWithValue("@CategoryId", productDTO.CategoryId);
                    command.Parameters.AddWithValue("@SubCategoryId", productDTO.SubCategoryId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageUrl", productDTO.ImageUrl);
                    command.Parameters.AddWithValue("@CreatedDate", productDTO.CreatedDate);
                    command.Parameters.AddWithValue("@ModifiedDate", productDTO.ModifiedDate ?? (object)DBNull.Value);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }

        // POST: api/ProductDTOes
        [HttpPost]
        public async Task<ActionResult> PostProductDTO(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest(new { message = "ProductDTO data is null." });
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Products (Name, Description, Price, StockQuantity, CategoryId, SubCategoryId, ImageUrl, CreatedDate, ModifiedDate) 
                  VALUES (@Name, @Description, @Price, @StockQuantity, @CategoryId, @SubCategoryId, @ImageUrl, @CreatedDate, @ModifiedDate)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", productDTO.Name);
                    command.Parameters.AddWithValue("@Description", productDTO.Description);
                    command.Parameters.AddWithValue("@Price", productDTO.Price);
                    command.Parameters.AddWithValue("@StockQuantity", productDTO.StockQuantity);
                    command.Parameters.AddWithValue("@CategoryId", productDTO.CategoryId);
                    command.Parameters.AddWithValue("@SubCategoryId", productDTO.SubCategoryId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageUrl", productDTO.ImageUrl);
                    command.Parameters.AddWithValue("@CreatedDate", productDTO.CreatedDate);
                    command.Parameters.AddWithValue("@ModifiedDate", productDTO.ModifiedDate ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { message = "ProductDTO created successfully." });
        }

        // DELETE: api/ProductDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDTO(long id)  // Change parameter type to long
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Products WHERE Id = @Id";

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
