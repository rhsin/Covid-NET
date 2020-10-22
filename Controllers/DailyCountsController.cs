using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Covid.Data;
using Covid.Models;
using Covid.Services;
using Covid.Repositories;

namespace Covid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICsvImporter _csvImporter;
        private readonly IDailyCountRepository _dailyCountRepository;

        public DailyCountsController(ApplicationDbContext context, ICsvImporter csvImporter,
            IDailyCountRepository dailyCountRepository)
        {
            _context = context;
            _csvImporter = csvImporter;
            _dailyCountRepository = dailyCountRepository;
        }

        // POST: api/DailyCounts/Import
        [HttpPost("Import")]
        public async Task<IActionResult> ImportDailyCounts()
        {
            return Ok(await _csvImporter.ImportDailyCounts(_context));
        }

        // GET: api/DailyCounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetDailyCount()
        {
            return await _dailyCountRepository.GetDailyCounts()
                .Take(100)
                .ToListAsync();
        }

        // GET: api/DailyCounts/Filter
        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Filter(string county, string state)
        {
            return await _dailyCountRepository.Filter(county, state)
                .Take(100)
                .ToListAsync();
        }

        // GET: api/DailyCounts/5
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

        // PUT: api/DailyCounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

        // POST: api/DailyCounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DailyCount>> PostDailyCount(DailyCount dailyCount)
        {
            _context.DailyCount.Add(dailyCount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyCount", new { id = dailyCount.Id }, dailyCount);
        }

        // DELETE: api/DailyCounts/5
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
