using Covid.Data;
using Covid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Controllers.CRUD
{
    [Authorize]
    [Route("CRUD/[controller]")]
    [ApiController]
    public class CountListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // PUT: CRUD/CountLists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountList(int id, CountList countList)
        {
            if (id != countList.Id)
            {
                return BadRequest();
            }

            _context.Entry(countList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountListExists(id))
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

        // POST: CRUD/CountLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CountList>> PostCountList(CountList countList)
        {
            _context.CountList.Add(countList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountList", new { id = countList.Id }, countList);
        }

        // DELETE: CRUD/CountLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountList>> DeleteCountList(int id)
        {
            var countList = await _context.CountList.FindAsync(id);
            if (countList == null)
            {
                return NotFound();
            }

            _context.CountList.Remove(countList);
            await _context.SaveChangesAsync();

            return countList;
        }

        private bool CountListExists(int id)
        {
            return _context.CountList.Any(e => e.Id == id);
        }
    }
}
