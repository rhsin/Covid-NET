﻿using Covid.DTO;
using Covid.Repositories;
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

            return this.ApiResponse("All CountLists", countLists);
        }

        // GET: api/CountLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountListDTO>> GetCountList(int id)
        {
            var countLists = await _countListRepository.GetCountLists();
                
            var countList = countLists.First(cl => cl.Id == id);

            if (countList == null)
            {
                return NotFound();
            }

            return Ok(new { Method = "Find By Id", Data = countList });
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

        private ActionResult<IEnumerable<CountListDTO>> ApiResponse(string method,
            IEnumerable<CountListDTO> countLists)
        {
            return Ok(new
            {
                Method = method,
                Count = countLists.Count(),
                Data = countLists
            });
        }
    }
}
