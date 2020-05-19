using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentRental.Models
{
    [Table("Apartment", Schema ="dbo")]
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Area { get; set; }
        public decimal MonthlyPrice { get; set; }
        public int Rooms { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DateAdded { get; set; }
        //public Realtor AssignedRealtor { get; set; }

    }
}
