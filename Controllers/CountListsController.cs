using Covid.Data;
using Covid.Models;
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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICountListRepository _countListRepository;

        public CountListsController(ApplicationDbContext context, ICountListRepository countListRepository)
        {
            _context = context;
            _countListRepository = countListRepository;
        }

        // GET: api/CountLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountList>>> GetCountList()
        {
            var countLists = await _countListRepository.GetCountLists().ToListAsync();

            return this.ApiResponse("All CountLists", countLists);
        }

        // GET: api/CountLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountList>> GetCountList(int id)
        {
            var countList = await _countListRepository.GetCountLists().SingleAsync(cl => cl.Id == id);

            if (countList == null)
            {
                return NotFound();
            }

            return countList;
        }

        // POST: api/CountLists/DailyCount/Add/5/1
        [HttpPost("DailyCount/Add/{listId}/{countId}")]
        public async Task<ActionResult> AddDailyCount(int listId, int countId)
        {
            await _countListRepository.AddDailyCount(listId, countId);

            return Ok($"DailyCount {countId} Added To CountList {listId}!");
        }

        // POST: api/CountLists/DailyCount/Remove/5/1
        [HttpPost("DailyCount/Remove/{listId}/{countId}")]
        public async Task<ActionResult> RemoveDailyCount(int listId, int countId)
        {
            await _countListRepository.RemoveDailyCount(listId, countId);

            return Ok($"DailyCount {countId} Removed From CountList {listId}!");
        }

        // PUT: api/CountLists/5
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

        // POST: api/CountLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CountList>> PostCountList(CountList countList)
        {
            _context.CountList.Add(countList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountList", new { id = countList.Id }, countList);
        }

        // DELETE: api/CountLists/5
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

        private ActionResult<IEnumerable<CountList>> ApiResponse(string method,
            IEnumerable<CountList> countLists)
        {
            return Ok(new
            {
                Method = method,
                Count = countLists.Count(),
                Data = countLists
            });
        }

        private bool CountListExists(int id)
        {
            return _context.CountList.Any(e => e.Id == id);
        }
    }
}
