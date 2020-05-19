using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApartmentRental.Models;

namespace ApartmentRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly ApartmentContext _context;

        public ApartmentController(ApartmentContext context)
        {
            _context = context;
        }

        // GET: api/Apartment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartment()
        {
            return await _context.Apartments.ToListAsync();
        }

        // GET: api/Apartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);

            if (apartment == null)
            {
                return NotFound();
            }

            return apartment;
        }

        // PUT: api/Apartment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return BadRequest();
            }

            _context.Entry(apartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Apartment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApartment), new { id = apartment.Id }, apartment);
        }

        // DELETE: api/Apartment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Apartment>> DeleteApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return apartment;
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}
