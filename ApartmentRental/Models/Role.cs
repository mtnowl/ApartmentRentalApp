using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApartmentRental.Models
{
    [Table("Role", Schema = "dbo")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
