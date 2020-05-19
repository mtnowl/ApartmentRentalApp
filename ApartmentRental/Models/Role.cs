using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApartmentRental.Models
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Client = "Client";
        public const string Realtor = "Realtor";
    }
}
