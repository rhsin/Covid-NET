using Covid.Data;
using Covid.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Repositories
{
    public interface IDailyCountRepository
    {
        public IQueryable<DailyCount> GetDailyCounts();
        public Task<IEnumerable<DailyCount>> Filter(string county, string state);
        public Task<IEnumerable<DailyCount>> Range(string column, int min, int max);
        public Task<IEnumerable<DailyCount>> DateRange(int month);
        public Task<IEnumerable<DailyCount>> Query(string county, string state, string order,
            int month, string column, int limit);
    }

    public class DailyCountRepository : IDailyCountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public DailyCountRepository(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IQueryable<DailyCount> GetDailyCounts()
        {
            var dailyCounts = from dc in _context.DailyCount
                              select dc;

            return dailyCounts.Take(100);
        }

        public async Task<IEnumerable<DailyCount>> Filter(string county, string state)
        {
            var parameters = new { County = $"%{county}%", State = $"%{state}%" };

            var sql = @"SELECT TOP 200 *
                        FROM DailyCount 
                        WHERE LOWER(County) LIKE LOWER(@County) 
                        AND LOWER(State) LIKE LOWER(@State)
                        ORDER BY County";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var dailyCounts = await connection.QueryAsync<DailyCount>(sql, parameters);

                return dailyCounts;
            }
        }

        public async Task<IEnumerable<DailyCount>> Range(string column, int min, int max)
        {
            var parameters = new { Min = min, Max = max };

            var sql = $@"SELECT TOP 100 *
                      FROM DailyCount
                      WHERE {column} BETWEEN @Min AND @Max
                      ORDER BY {column} DESC";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var dailyCounts = await connection.QueryAsync<DailyCount>(sql, parameters);

                return dailyCounts;
            }
        }

        public async Task<IEnumerable<DailyCount>> DateRange(int month)
        {
            var parameters = new { Month = month };

            var sql = $@"SELECT TOP 50 *
                      FROM DailyCount
                      WHERE MONTH(Date) = @Month
                      ORDER BY Date";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var dailyCounts = await connection.QueryAsync<DailyCount>(sql, parameters);

                return dailyCounts;
            }
        }

        public async Task<IEnumerable<DailyCount>> Query(string county, string state, string order,
            int month, string column, int limit)
        {
            var parameters = new {
                County = $"%{county}%",
                State = $"%{state}%",
                Month = month,
                Order = order,
                Limit = limit
            };

            var sql = $@"SELECT TOP (@Limit) *
                        FROM DailyCount 
                        WHERE LOWER(County) LIKE LOWER(@County) 
                        AND LOWER(State) LIKE LOWER(@State)
                        AND MONTH(Date) = @Month
                        ORDER BY {column} {order}";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var dailyCounts = await connection.QueryAsync<DailyCount>(sql, parameters);

                return dailyCounts;
            }
        }
    }
}


//public IQueryable<DailyCount> Filter(string county, string state)
//{
//    var dailyCounts = from dc in _context.DailyCount
//                select dc;

//    if (!String.IsNullOrEmpty(county))
//    {
//        dailyCounts = dailyCounts.Where(dc => dc.County.Contains(county));
//    }

//    if (!String.IsNullOrEmpty(state))
//    {
//        dailyCounts = dailyCounts.Where(dc => dc.State.Contains(state));
//    }

//    return dailyCounts.OrderBy(dc => dc.County);
//}