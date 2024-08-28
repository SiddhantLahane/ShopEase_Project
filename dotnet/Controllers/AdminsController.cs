using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecomercewebapi.Data;
using ecomercewebapi.Models;
using ecomercewebapi.Dtos;
using ecomercewebapi.Services;

namespace ecomercewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        // private readonly ecomercewebapiContext _context;
        private readonly AdminService _adminService;
        public AdminsController(AdminService adminService)
        {
            // _context = context;
            _adminService = adminService;
        }

        /* // GET: api/Admins
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
         {
             return await _context.Admin.ToListAsync();
         }

         // GET: api/Admins/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Admin>> GetAdmin(int id)
         {
             var admin = await _context.Admin.FindAsync(id);

             if (admin == null)
             {
                 return NotFound();
             }

             return admin;
         }

         // PUT: api/Admins/5
         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
         public async Task<IActionResult> PutAdmin(int id, Admin admin)
         {
             if (id != admin.id)
             {
                 return BadRequest();
             }

             _context.Entry(admin).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!AdminExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }*/

        /*[HttpPost]
        public async Task<IActionResult> Register(AdminLoginDto adminDto)
        {
            await _adminService.RegisterAdmin(adminDto);
            return Ok(); // or appropriate response
        }*/

        [HttpPost]
        public  IActionResult Login(AdminLoginDto adminDto)
        {
            var admin =_adminService.AuthenticateAdmin(adminDto.username, adminDto.password);
            if (admin == null)
            {
                return Unauthorized();
            }

            return Ok(admin); // or appropriate response
        }


        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPost]
         public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
         {
             _context.Admin.Add(admin);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetAdmin", new { id = admin.id }, admin);
         }

         // DELETE: api/Admins/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteAdmin(int id)
         {
             var admin = await _context.Admin.FindAsync(id);
             if (admin == null)
             {
                 return NotFound();
             }

             _context.Admin.Remove(admin);
             await _context.SaveChangesAsync();

             return NoContent();
         }

         private bool AdminExists(int id)
         {
             return _context.Admin.Any(e => e.id == id);
         }
     }*/
    }
}