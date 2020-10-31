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

            return Ok(new ApiResponse().Json("All AppUsers", appUsers));
        }

        // GET: api/AppUsers/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAppUser(int id)
        {
            var appUsers = await _appUserRepository.GetAppUsers();

            try
            {
                var appUser = appUsers.First(au => au.AccountId == id);

                return Ok(new { Method = "Find By Id", Data = appUser });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/AppUsers/Role/User
        [Authorize]
        [HttpGet("Role/{role}")]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUserRole(string role)
        {
            var appUsers = await _appUserRepository.GetAppUserRole(role);

            return Ok(new ApiResponse().Json("Find By Role", appUsers));
        }
    }
}
