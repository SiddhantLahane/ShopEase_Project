using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long OrderId { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }

    [Required]
    public long ProductId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}
