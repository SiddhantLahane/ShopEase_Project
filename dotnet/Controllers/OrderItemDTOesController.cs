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

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            var orderItems = new List<OrderItemDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, OrderId, ProductId, Quantity, Price FROM OrderItems";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orderItems.Add(new OrderItemDTO
                        {
                            Id = reader.GetInt64("Id"),
                            OrderId = reader.GetInt64("OrderId"),
                            ProductId = reader.GetInt64("ProductId"),
                            Quantity = reader.GetInt32("Quantity"),
                            Price = reader.GetDecimal("Price")
                        });
                    }
                }
            }

            return Ok(orderItems);
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem(long id)
        {
            OrderItemDTO orderItem = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, OrderId, ProductId, Quantity, Price FROM OrderItems WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            orderItem = new OrderItemDTO
                            {
                                Id = reader.GetInt64("Id"),
                                OrderId = reader.GetInt64("OrderId"),
                                ProductId = reader.GetInt64("ProductId"),
                                Quantity = reader.GetInt32("Quantity"),
                                Price = reader.GetDecimal("Price")
                            };
                        }
                    }
                }
            }

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(long id, OrderItemDTO orderItemDTO)
        {
            if (id != orderItemDTO.Id)
            {
                return BadRequest();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE OrderItems 
                             SET OrderId = @OrderId, ProductId = @ProductId, 
                                 Quantity = @Quantity, Price = @Price 
                             WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", orderItemDTO.Id);
                    command.Parameters.AddWithValue("@OrderId", orderItemDTO.OrderId);
                    command.Parameters.AddWithValue("@ProductId", orderItemDTO.ProductId);
                    command.Parameters.AddWithValue("@Quantity", orderItemDTO.Quantity);
                    command.Parameters.AddWithValue("@Price", orderItemDTO.Price);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult> PostOrderItem(OrderItemDTO orderItemDTO)
        {
            if (orderItemDTO == null)
            {
                return BadRequest(new { message = "OrderItemDTO data is null." });
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) 
                             VALUES (@OrderId, @ProductId, @Quantity, @Price)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderItemDTO.OrderId);
                    command.Parameters.AddWithValue("@ProductId", orderItemDTO.ProductId);
                    command.Parameters.AddWithValue("@Quantity", orderItemDTO.Quantity);
                    command.Parameters.AddWithValue("@Price", orderItemDTO.Price);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { message = "OrderItem created successfully." });
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM OrderItems WHERE Id = @Id";

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
