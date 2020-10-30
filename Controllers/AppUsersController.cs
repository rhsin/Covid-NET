using Covid.DTO;
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
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUsersController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUser()
        {
            var appUsers = await _appUserRepository.GetAppUsers();

            return Ok(this.ApiResponse("All AppUsers", appUsers));
        }

        // GET: api/AppUsers/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAppUser(int id)
        {
            var appUsers = await _appUserRepository.GetAppUsers();

            var appUser = appUsers.First(au => au.AccountId == id);

            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(new { Method = "Find By Id", Data = appUser });
        }

        // GET: api/AppUsers/Role/User
        [Authorize]
        [HttpGet("Role/{role}")]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUserRole(string role)
        {
            var appUsers = await _appUserRepository.GetAppUserRole(role);

            return Ok(this.ApiResponse("Find By Role", appUsers));
        }

        public object ApiResponse(string method, IEnumerable<AppUserDTO> appUsers)
        {
            return new
            {
                Method = method,
                Count = appUsers.Count(),
                Data = appUsers
            };
        }
    }
}
