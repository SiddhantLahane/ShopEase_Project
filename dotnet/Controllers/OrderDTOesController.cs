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
    public class OrdersController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=E_Commerce_Website;User ID=root;Password=Sayu311@;";

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = new List<OrderDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT o.Id, o.UserId, o.ShipAddressId, o.ShipperId, o.OrderDate, o.TotalAmount, o.Status
                             FROM Orders o";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var order = new OrderDTO
                        {
                            Id = reader.GetInt64("Id"),
                            UserId = reader.GetInt64("UserId"),
                            ShipAddressId = reader.GetInt64("ShipAddressId"),
                            ShipperId = reader.GetInt64("ShipperId"),
                            OrderDate = reader.GetDateTime("OrderDate"),
                            TotalAmount = reader.GetDecimal("TotalAmount"),
                            Status = reader.GetString("Status"),
                            OrderItems = new List<OrderItemDTO>()
                        };

                        orders.Add(order);
                    }
                }
            }

            foreach (var order in orders)
            {
                order.OrderItems = await GetOrderItemsByOrderId(order.Id);
            }

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(long id)
        {
            OrderDTO order = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT Id, UserId, ShipAddressId, ShipperId, OrderDate, TotalAmount, Status 
                             FROM Orders WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            order = new OrderDTO
                            {
                                Id = reader.GetInt64("Id"),
                                UserId = reader.GetInt64("UserId"),
                                ShipAddressId = reader.GetInt64("ShipAddressId"),
                                ShipperId = reader.GetInt64("ShipperId"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                TotalAmount = reader.GetDecimal("TotalAmount"),
                                Status = reader.GetString("Status"),
                                OrderItems = new List<OrderItemDTO>()
                            };
                        }
                    }
                }
            }

            if (order == null)
            {
                return NotFound();
            }

            order.OrderItems = await GetOrderItemsByOrderId(order.Id);

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Orders 
                             SET UserId = @UserId, ShipAddressId = @ShipAddressId, 
                                 ShipperId = @ShipperId, OrderDate = @OrderDate, 
                                 TotalAmount = @TotalAmount, Status = @Status
                             WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", orderDTO.Id);
                    command.Parameters.AddWithValue("@UserId", orderDTO.UserId);
                    command.Parameters.AddWithValue("@ShipAddressId", orderDTO.ShipAddressId);
                    command.Parameters.AddWithValue("@ShipperId", orderDTO.ShipperId);
                    command.Parameters.AddWithValue("@OrderDate", orderDTO.OrderDate);
                    command.Parameters.AddWithValue("@TotalAmount", orderDTO.TotalAmount);
                    command.Parameters.AddWithValue("@Status", orderDTO.Status);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }

                await UpdateOrderItems(orderDTO.OrderItems, id);
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest(new { message = "OrderDTO data is null." });
            }

            long newOrderId;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Orders (UserId, ShipAddressId, ShipperId, OrderDate, TotalAmount, Status) 
                             VALUES (@UserId, @ShipAddressId, @ShipperId, @OrderDate, @TotalAmount, @Status);
                             SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", orderDTO.UserId);
                    command.Parameters.AddWithValue("@ShipAddressId", orderDTO.ShipAddressId);
                    command.Parameters.AddWithValue("@ShipperId", orderDTO.ShipperId);
                    command.Parameters.AddWithValue("@OrderDate", orderDTO.OrderDate);
                    command.Parameters.AddWithValue("@TotalAmount", orderDTO.TotalAmount);
                    command.Parameters.AddWithValue("@Status", orderDTO.Status);

                    newOrderId = (long)(await command.ExecuteScalarAsync());
                }

                foreach (var orderItem in orderDTO.OrderItems)
                {
                    orderItem.OrderId = newOrderId;
                    await InsertOrderItem(orderItem);
                }
            }

            return Ok(new { message = "Order created successfully.", OrderId = newOrderId });
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Orders WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }

                await DeleteOrderItems(id);
            }

            return NoContent();
        }

        // Helper method to get OrderItems by OrderId
        private async Task<List<OrderItemDTO>> GetOrderItemsByOrderId(long orderId)
        {
            var orderItems = new List<OrderItemDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, OrderId, ProductId, Quantity, Price FROM OrderItems WHERE OrderId = @OrderId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

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
            }

            return orderItems;
        }

        // Helper method to update OrderItems
        private async Task UpdateOrderItems(IEnumerable<OrderItemDTO> orderItems, long orderId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Delete existing OrderItems for the order
                string deleteQuery = "DELETE FROM OrderItems WHERE OrderId = @OrderId";

                using (var deleteCommand = new MySqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@OrderId", orderId);
                    await deleteCommand.ExecuteNonQueryAsync();
                }

                // Insert updated OrderItems
                foreach (var orderItem in orderItems)
                {
                    await InsertOrderItem(orderItem);
                }
            }
        }

        // Helper method to insert an OrderItem
        private async Task InsertOrderItem(OrderItemDTO orderItem)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price) 
                             VALUES (@OrderId, @ProductId, @Quantity, @Price)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderItem.OrderId);
                    command.Parameters.AddWithValue("@ProductId", orderItem.ProductId);
                    command.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
                    command.Parameters.AddWithValue("@Price", orderItem.Price);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Helper method to delete OrderItems by OrderId
        private async Task DeleteOrderItems(long orderId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM OrderItems WHERE OrderId = @OrderId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }

}
