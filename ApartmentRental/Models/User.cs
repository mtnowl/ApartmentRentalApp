using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApartmentRental.Models
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
