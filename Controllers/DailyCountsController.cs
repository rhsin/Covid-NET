using Covid.Data;
using Covid.Models;
using Covid.Services;
using Covid.Repositories;
using Microsoft.AspNetCore.Authorization;
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

            return this.ApiResponse($"Filter By {county}, {state}", dailyCounts);
        }

        // GET: api/DailyCounts/Range/Cases
        [HttpGet("Range/{column?}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Range(string column = "Cases",
            int min = 0, int max = 200000)
        {
            var dailyCounts = await _dailyCountRepository.Range(column, min, max);

            return this.ApiResponse($"Range By {column}", dailyCounts);
        }

        // GET: api/DailyCounts/DateRange/5
        [HttpGet("DateRange/{month}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> DateRange(int month)
        {
            var dailyCounts = await _dailyCountRepository.DateRange(month);

            return this.ApiResponse($"Dates In Month {month}", dailyCounts);
        }

        // GET: api/DailyCounts/Query
        [HttpGet("Query")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Query(string county, string state, string order,
            int month = 9, string column = "Date", int limit = 100)
        {
            var dailyCounts = await _dailyCountRepository.Query(county, state, order, month, column, limit);

            return this.ApiResponse(
                $"Query By County: {county}, State: {state}, Order: {order}," +
                $" Month: {month}, Column: {column}, Limit: {limit}",
                dailyCounts
            );
        }

        // GET: api/DailyCounts/Max/Cases
        [HttpGet("Max/{column?}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetMax(string column = "Cases")
        {
            var dailyCount = await _dailyCountRepository.GetMax(column);

            return this.ApiResponse($"Max {column}", dailyCount);
        }

        // POST: api/DailyCounts/Import
        [Authorize]
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

        // POST: api/DailyCounts
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

        // DELETE: api/DailyCounts/5
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

        private ActionResult<IEnumerable<DailyCount>> ApiResponse(string method,
            IEnumerable<DailyCount> dailyCounts)
        {
            return Ok(new
            {
                Method = method,
                Count = dailyCounts.Count(),
                Data = dailyCounts
            });
        }

        private bool DailyCountExists(int id)
        {
            return _context.DailyCount.Any(e => e.Id == id);
        }
    }
}
