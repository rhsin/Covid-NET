using Covid.DTO;
using Covid.Repositories;
using Covid.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountListsController : ControllerBase
    {
        private readonly ICountListRepository _countListRepository;

        public CountListsController(ICountListRepository countListRepository)
        {
            _countListRepository = countListRepository;
        }

        // GET: api/CountLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountListDTO>>> GetCountList()
        {
            var countLists = await _countListRepository.GetCountLists();

            return Ok(new ApiResponse("All CountLists", countLists));
        }

        // GET: api/CountLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountListDTO>> GetCountList(int id)
        {
            var countLists = await _countListRepository.GetCountLists();
            
            try
            {
                var countList = countLists.First(cl => cl.Id == id);

                return Ok(new { Method = "Find By Id", Data = countList });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/CountLists/DailyCount/Add/5/1
        [Authorize]
        [HttpPost("DailyCount/Add/{listId}/{countId}")]
        public async Task<ActionResult> AddDailyCount(int listId, int countId)
        {
            await _countListRepository.AddDailyCount(listId, countId);

            return Ok($"DailyCount {countId} Added To CountList {listId}!");
        }

        // POST: api/CountLists/DailyCount/Remove/5/1
        [Authorize]
        [HttpPost("DailyCount/Remove/{listId}/{countId}")]
        public async Task<ActionResult> RemoveDailyCount(int listId, int countId)
        {
            await _countListRepository.RemoveDailyCount(listId, countId);

            return Ok($"DailyCount {countId} Removed From CountList {listId}!");
        }
    }
}
