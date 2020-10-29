using Covid.Models;
using Covid.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCountsController : ControllerBase
    {
        private readonly IDailyCountRepository _dailyCountRepository;

        public DailyCountsController(IDailyCountRepository dailyCountRepository)
        {
            _dailyCountRepository = dailyCountRepository;
        }

        // GET: api/DailyCounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetDailyCount()
        {
            var dailyCounts = await _dailyCountRepository.GetDailyCounts();

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
    }
}
