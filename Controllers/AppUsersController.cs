using Covid.Data;
using Covid.Models;
using Covid.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Covid.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppUserRepository _appUserRepository;

        public AppUsersController(ApplicationDbContext context, IAppUserRepository appUserRepository)
        {
            _context = context;
            _appUserRepository = appUserRepository;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUser()
        {
            return await _appUserRepository.GetAppUsers().ToListAsync();
        }

        // GET: api/AppUsers/ec676d34-b2e3-4187-8736-312e57efa1ed
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAppUser(int id)
        {
            var appUser = await _appUserRepository.GetAppUsers()
                .SingleAsync(au => au.AccountId == id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }
    }
}
