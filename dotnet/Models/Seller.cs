using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Seller
{
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string ContactNumber { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    public string Address { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
