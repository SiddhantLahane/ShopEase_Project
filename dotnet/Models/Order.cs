using ecomercewebapi.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

public class Order
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual ecomercewebapi.Models.Users User { get; set; }

    [Required]
    public long ShipAddressId { get; set; }

    [ForeignKey("ShipAddressId")]
    public virtual Address ShipAddress { get; set; }

    [Required]
    public long ShipperId { get; set; }

    [ForeignKey("ShipperId")]
    public virtual Shipper Shipper { get; set; }

    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalAmount { get; set; }

    public string Status { get; set; }  // e.g., Pending, Shipped, Delivered, Cancelled

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
