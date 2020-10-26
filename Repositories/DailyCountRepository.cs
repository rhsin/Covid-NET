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
        public Task<IEnumerable<DailyCount>> GetMax(string column);
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

            string sql = @"SELECT TOP 200 *
                           FROM DailyCount 
                           WHERE LOWER(County) LIKE LOWER(@County) 
                           AND LOWER(State) LIKE LOWER(@State)
                           ORDER BY County";

            return await this.ExecuteCountQuery(sql, parameters);
        }

        public async Task<IEnumerable<DailyCount>> Range(string column, int min, int max)
        {
            var parameters = new { Min = min, Max = max };

            string sql = $@"SELECT TOP 100 *
                            FROM DailyCount
                            WHERE {column} BETWEEN @Min AND @Max
                            ORDER BY {column} DESC";

            return await this.ExecuteCountQuery(sql, parameters);
        }

        public async Task<IEnumerable<DailyCount>> DateRange(int month)
        {
            var parameters = new { Month = month };

            string sql = $@"SELECT TOP 50 *
                            FROM DailyCount
                            WHERE MONTH(Date) = @Month
                            ORDER BY Date";

            return await this.ExecuteCountQuery(sql, parameters);
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

            string sql = $@"SELECT TOP (@Limit) *
                            FROM DailyCount 
                            WHERE LOWER(County) LIKE LOWER(@County) 
                            AND LOWER(State) LIKE LOWER(@State)
                            AND MONTH(Date) = @Month
                            ORDER BY {column} {order}";

            return await this.ExecuteCountQuery(sql, parameters);
        }

        public async Task<IEnumerable<DailyCount>> GetMax(string column)
        {
            string sql = $@"SELECT *
                            FROM DailyCount
                            WHERE {column} = 
                            (SELECT MAX({column})
                            FROM DailyCount)";

            return await this.ExecuteCountQuery(sql, null);
        }

        private async Task<IEnumerable<DailyCount>> ExecuteCountQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var dailyCounts = await connection.QueryAsync<DailyCount>(sql, parameters);

                return dailyCounts;
            }
        }
    }
}
