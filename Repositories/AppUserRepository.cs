using Covid.Data;
using Covid.DTO;
using Covid.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Repositories
{
    public interface IAppUserRepository
    {
        public IQueryable<AppUserDTO> GetAppUsers();
        public Task<IEnumerable<AppUserDTO>> GetAppUserRole(string role);
        public Task<object> GetAppUserDetails();
    }

    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AppUserRepository(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
                    Role = au.Role,
                    CountLists = au.CountLists
                });
        }

        public async Task<object> GetAppUserDetails()
        {
            var parameters = new {  };

            string sql = @"SELECT *
                           FROM AspNetUsers
                           INNER JOIN CountList 
                           ON AspNetUsers.Id = CountList.AppUserId";

            return await this.ExecuteQuery(sql, parameters);
        }

        public async Task<IEnumerable<AppUserDTO>> GetAppUserRole(string role)
        {
            var parameters = new { Role = role };

            string sql = @"SELECT *
                           FROM AspNetUsers
                           WHERE Role = @Role";

            return await this.ExecuteUserQuery(sql, parameters);
        }

        private async Task<object> ExecuteQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var result = await connection.QueryAsync<object>(sql, parameters);

                return result;
            }
        }

        private async Task<IEnumerable<AppUserDTO>> ExecuteUserQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var appUsers = await connection.QueryAsync<AppUserDTO>(sql, parameters);

                return appUsers;
            }
        }
    }
}
