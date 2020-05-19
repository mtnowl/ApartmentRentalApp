using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Models
{
    public class ApartmentContext : DbContext
    {
        public ApartmentContext(DbContextOptions<ApartmentContext> options)
            :base(options) 
        { 
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
