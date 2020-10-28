using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid.Repositories
{
    public interface IQueryRepository
    {
        public Task<object> GetDetails();
        public Task<object> GetCasesData(string county, string state, int month);
    }

    public class QueryRepository : IQueryRepository
    {
        private readonly IConfiguration _config;

        public QueryRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<object> GetDetails()
        {
            var parameters = new { };

            string sql = @"SELECT u.AccountId, u.Name, u.County, u.Role, l.Id AS ListId,
                           c.Id AS CountId, c.Date, c.County, c.Cases
                           FROM AspNetUsers AS u
                           INNER JOIN CountList AS l
                           ON u.Id = l.AppUserId
                           INNER JOIN CountListDailyCount AS lc
                           ON l.Id = lc.CountListId
                           INNER JOIN DailyCount AS c
                           ON lc.DailyCountId = c.Id
                           ORDER BY Date";

            return await this.ExecuteQuery(sql, parameters);
        }

        public async Task<object> GetCasesData(string county, string state, int month)
        {
            var parameters = new
            {
                County = $"%{county}%",
                State = $"%{state}%",
                Month = month
            };

            string sqlData = $@"SELECT TOP 31 Cases
                                FROM DailyCount
                                WHERE LOWER(County) LIKE LOWER(@County) 
                                AND LOWER(State) LIKE LOWER(@State)
                                AND MONTH(Date) = @Month
                                ORDER BY Date";

            string sqlAvg = $@"SELECT AVG(Cases) AS Average
                               FROM 
                               (SELECT TOP 31 Cases
                               FROM DailyCount
                               WHERE LOWER(County) LIKE LOWER(@County) 
                               AND LOWER(State) LIKE LOWER(@State)
                               AND MONTH(Date) = @Month)
                               DailyCountAverage";

            var value = await this.ExecuteQuery(sqlAvg, parameters);
            var data = await this.ExecuteQuery(sqlData, parameters);

            return new { Value = value, Count = 31, Data = data };
        }

        private async Task<object> ExecuteQuery(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var result = await connection.QueryAsync<object>(sql, parameters);

                return result;
            }
        }
    }
}
