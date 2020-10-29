using Covid.Data;
using Covid.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Covid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICsvImporter _csvImporter;

        public ImportController(ApplicationDbContext context, ICsvImporter csvImporter)
        {
            _context = context;
            _csvImporter = csvImporter;
        }

        // POST: api/Import/DailyCounts
        [Authorize]
        [HttpPost("DailyCounts")]
        public async Task<ActionResult<string>> ImportDailyCounts()
        {
            return Ok(await _csvImporter.ImportDailyCounts(_context));
        }
    }
}
