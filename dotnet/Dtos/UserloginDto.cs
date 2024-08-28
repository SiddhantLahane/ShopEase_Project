using System.ComponentModel.DataAnnotations;

namespace ecomercewebapi.Dtos
{
    public class UserloginDto
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
