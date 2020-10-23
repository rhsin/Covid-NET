using Covid.Data;
using Covid.Models;
using Covid.Services;
using Covid.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/DailyCounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetDailyCount()
        {
            var dailyCounts = await _dailyCountRepository.GetDailyCounts().ToListAsync();

            return this.ApiResponse("All DailyCounts", dailyCounts);
        }

        // GET: api/DailyCounts/Filter
        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Filter(string county, string state)
        {
            var dailyCounts = await _dailyCountRepository.Filter(county, state);

            return this.ApiResponse("Filter By County, State", dailyCounts);
        }

        // GET: api/DailyCounts/Range/Cases
        [HttpGet("Range/{column}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Range(string col, int min, int max)
        {
            string column = "Cases";

            var dailyCounts = await _dailyCountRepository.Range(column, min, max);

            return this.ApiResponse($"Range By {column}", dailyCounts);
        }

        public ActionResult<IEnumerable<DailyCount>> ApiResponse(string method,
            IEnumerable<DailyCount> dailyCounts)
        {
            return Ok(new
            {
                Method = method,
                Count = dailyCounts.Count(),
                Data = dailyCounts
            });
        }

        // POST: api/DailyCounts/Import
        [HttpPost("Import")]
        public async Task<ActionResult<string>> ImportDailyCounts()
        {
            return Ok(await _csvImporter.ImportDailyCounts(_context));
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
