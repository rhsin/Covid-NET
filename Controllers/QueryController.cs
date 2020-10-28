using Covid.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryRepository _queryRepository;

        public QueryController(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        // GET: api/Query/Details
        [HttpGet("Details")]
        public async Task<ActionResult<object>> GetDetails()
        {
            var data = await _queryRepository.GetDetails();

            return Ok(new { Method = "Query Details", Data = data });
        }

        // GET: api/Query/Data/Cases/7
        [HttpGet("Data/Cases/{month?}")]
        public async Task<ActionResult<object>> GetCasesData(string county, string state, int month = 9)
        {
            var data = await _queryRepository.GetCasesData(county, state, month);

            return Ok(new { Method = "Query Cases Data By Month", Data = data });
        }
    }
}
