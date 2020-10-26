using Covid.Data;
using Covid.Models;
using System;
using System.Linq;

namespace Covid.Repositories
{
    public interface IAppUserRepository
    {
        public IQueryable<AppUserDTO> GetAppUsers();
    }

    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<AppUserDTO> GetAppUsers()
        {
            return _context.AppUser
                .Select(au => new AppUserDTO
                {
                    Id = au.Id,
                    UserName = au.UserName,
                    Role = au.Role
                });
        }
    }
}
