using Covid.Data;
using Covid.DTO;
using Covid.Models;
using Microsoft.EntityFrameworkCore;
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
                .Include(au => au.CountLists)
                .Select(au => new AppUserDTO
                {
                    Id = au.Id,
                    AccountId = au.AccountId,
                    UserName = au.UserName,
                    Name = au.Name,
                    County = au.County,
                    Role = au.Role
                });
        }
    }
}
