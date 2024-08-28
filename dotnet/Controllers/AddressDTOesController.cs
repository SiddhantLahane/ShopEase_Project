using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomercewebapi.Data;
using ecomercewebapi.Dtos;

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressDTOesController : ControllerBase
    {
        private readonly ecomercewebapiContext _context;

        public AddressDTOesController(ecomercewebapiContext context)
        {
            _context = context;
        }

        // GET: api/AddressDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressDTO()
        {
            return await _context.AddressDTO.ToListAsync();
        }

        // GET: api/AddressDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> GetAddressDTO(long id)
        {
            var addressDTO = await _context.AddressDTO.FindAsync(id);

            if (addressDTO == null)
            {
                return NotFound();
            }

            return addressDTO;
        }

        // PUT: api/AddressDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressDTO(long id, AddressDTO addressDTO)
        {
            if (id != addressDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressDTOExists(id))
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

        // POST: api/AddressDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressDTO>> PostAddressDTO(AddressDTO addressDTO)
        {
            _context.AddressDTO.Add(addressDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressDTO", new { id = addressDTO.Id }, addressDTO);
        }

        // DELETE: api/AddressDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressDTO(long id)
        {
            var addressDTO = await _context.AddressDTO.FindAsync(id);
            if (addressDTO == null)
            {
                return NotFound();
            }

            _context.AddressDTO.Remove(addressDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressDTOExists(long id)
        {
            return _context.AddressDTO.Any(e => e.Id == id);
        }
    }
}
