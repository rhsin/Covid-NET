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
    [Route("CRUD/[controller]")]
    [ApiController]
    public class DailyCountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyCountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CRUD/DailyCounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyCount>> GetDailyCount(int id)
        {
            var dailyCount = await _context.DailyCount.FindAsync(id);

            if (dailyCount == null)
            {
                return NotFound();
            }

            return dailyCount;
        }

        // PUT: CRUD/DailyCounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyCount(int id, DailyCount dailyCount)
        {
            if (id != dailyCount.Id)
            {
                return BadRequest();
            }

            _context.Entry(dailyCount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyCountExists(id))
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

        // POST: CRUD/DailyCounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DailyCount>> PostDailyCount(DailyCount dailyCount)
        {
            _context.DailyCount.Add(dailyCount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyCount", new { id = dailyCount.Id }, dailyCount);
        }

        // DELETE: CRUD/DailyCounts/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DailyCount>> DeleteDailyCount(int id)
        {
            var dailyCount = await _context.DailyCount.FindAsync(id);
            if (dailyCount == null)
            {
                return NotFound();
            }

            _context.DailyCount.Remove(dailyCount);
            await _context.SaveChangesAsync();

            return dailyCount;
        }

        private bool DailyCountExists(int id)
        {
            return _context.DailyCount.Any(e => e.Id == id);
        }
    }
}
