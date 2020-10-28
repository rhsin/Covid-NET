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
    public interface ICountListRepository
    {
        public Task<IEnumerable<CountListDTO>> GetCountLists();
        public Task AddDailyCount(int listId, int countId);
        public Task RemoveDailyCount(int listId, int countId);
    }

    public class CountListRepository : ICountListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public CountListRepository(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IEnumerable<CountListDTO>> GetCountLists()
        {
            return await _context.CountList
                .Include(cl => cl.CountListDailyCounts)
                .ThenInclude(cd => cd.DailyCount)
                .Include(cl => cl.AppUser)
                .Select(cl => new CountListDTO
                {
                    Id = cl.Id,
                    CountListDailyCounts = cl.CountListDailyCounts,
                    AppUserDTO = new AppUserDTO
                    {
                        Id = cl.AppUser.Id,
                        AccountId = cl.AppUser.AccountId,
                        UserName = cl.AppUser.UserName,
                        Name = cl.AppUser.Name,
                        County = cl.AppUser.County,
                        Role = cl.AppUser.Role
                    }
                })
                .ToListAsync();
        }

        public async Task AddDailyCount(int listId, int countId)
        {
            var parameters = new { ListId = listId, CountId = countId };

            string sql = @"INSERT INTO CountListDailyCount
                           VALUES (@ListId, @CountId)";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task RemoveDailyCount(int listId, int countId)
        {
            var parameters = new { ListId = listId, CountId = countId };

            string sql = @"DELETE FROM CountListDailyCount
                           WHERE CountListId = @ListId
                           AND DailyCountId = @CountId";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        private async Task<IEnumerable<CountList>> ExecuteListQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var countLists = await connection.QueryAsync<CountList>(sql, parameters);

                return countLists;
            }
        }
    }
}
