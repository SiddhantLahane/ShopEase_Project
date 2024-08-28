using System.ComponentModel.DataAnnotations;

namespace ecomercewebapi.Models
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        //public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
