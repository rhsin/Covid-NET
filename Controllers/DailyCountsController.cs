using Covid.Models;
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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCountsController : ControllerBase
    {
        private readonly IDailyCountRepository _dailyCountRepository;
        private readonly IInputValidator _inputValidator;

        public DailyCountsController(IDailyCountRepository dailyCountRepository, 
            IInputValidator inputValidator)
        {
            _dailyCountRepository = dailyCountRepository;
            _inputValidator = inputValidator;
        }

        // GET: api/DailyCounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetDailyCount()
        {
            var dailyCounts = await _dailyCountRepository.GetDailyCounts();

            return Ok(this.ApiResponse("All DailyCounts", dailyCounts));
        }

        // GET: api/DailyCounts/Filter
        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Filter(string county, string state)
        {
            if (!_inputValidator.IsValidState(state))
            {
                return BadRequest("Invalid State Input");
            }

            var dailyCounts = await _dailyCountRepository.Filter(county, state);

            return Ok(this.ApiResponse($"Filter By {county}, {state}", dailyCounts));
        }

        // GET: api/DailyCounts/Range/Cases
        [HttpGet("Range/{column?}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Range(string column = "Cases",
            int min = 0, int max = 200000)
        {
            if (!_inputValidator.IsValidColumn(column))
            {
                return BadRequest("Invalid Column Input");
            }

            var dailyCounts = await _dailyCountRepository.Range(column, min, max);

            return Ok(this.ApiResponse($"Range By {column}", dailyCounts));
        }

        // GET: api/DailyCounts/DateRange/5
        [HttpGet("DateRange/{month}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> DateRange(int month = 9)
        {
            if (!_inputValidator.IsValidMonth(month))
            {
                return BadRequest("Invalid Month Input");
            }

            var dailyCounts = await _dailyCountRepository.DateRange(month);

            return Ok(this.ApiResponse($"Dates In Month {month}", dailyCounts));
        }

        // GET: api/DailyCounts/Query
        [HttpGet("Query")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> Query(string county, string state, 
            string order = "asc", int month = 9, string column = "Date", int limit = 100)
        {
            if (!_inputValidator.IsValidCounty(county))
            {
                return BadRequest("Invalid County Input");
            }

            if (!_inputValidator.IsValidState(state))
            {
                return BadRequest("Invalid State Input");
            }

            if (!_inputValidator.IsValidMonth(month))
            {
                return BadRequest("Invalid Month Input");
            }

            if (!_inputValidator.IsValidOrder(order))
            {
                return BadRequest("Invalid Order Input");
            }

            if (!_inputValidator.IsValidColumn(column))
            {
                return BadRequest("Invalid Column Input");
            }

            var dailyCounts = await _dailyCountRepository.Query(county, state, order, month, column, limit);

            return Ok(this.ApiResponse(
                $"Query By County: {county}, State: {state}, Order: {order}," +
                $" Month: {month}, Column: {column}, Limit: {limit}",
                dailyCounts
            ));
        }

        // GET: api/DailyCounts/Max/Cases
        [HttpGet("Max/{column?}")]
        public async Task<ActionResult<IEnumerable<DailyCount>>> GetMax(string column = "Cases")
        {
            if (!_inputValidator.IsValidColumn(column))
            {
                return BadRequest("Invalid Column Input");
            }

            var dailyCount = await _dailyCountRepository.GetMax(column);

            return Ok(this.ApiResponse($"Max {column}", dailyCount));
        }

        public object ApiResponse(string method, IEnumerable<DailyCount> dailyCounts)
        {
            return new
            {
                Method = method,
                Count = dailyCounts.Count(),
                Data = dailyCounts
            };
        }
    }
}
