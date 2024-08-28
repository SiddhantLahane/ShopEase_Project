using System.ComponentModel.DataAnnotations;

namespace ecomercewebapi.Models
{
    public class Users
    {
  
            [Key]
            public int Id { get; set; } // Matches the key type used in Users class
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }

            public string City { get; set; }
            public string Pincode { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }


