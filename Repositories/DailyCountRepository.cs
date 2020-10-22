using System;
using System.Collections.Generic;
using System.Linq;
using Covid.Data;
using Covid.Models;

namespace Covid.Repositories
{
    public interface IDailyCountRepository
    {
        public IQueryable<DailyCount> GetDailyCounts();
        public IQueryable<DailyCount> Filter(string county, string state);
    }

    public class DailyCountRepository : IDailyCountRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyCountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<DailyCount> GetDailyCounts()
        {
            var query = from dc in _context.DailyCount
                        select dc;

            return query;
        }

        public IQueryable<DailyCount> Filter(string county, string state)
        {
            var query = from dc in _context.DailyCount
                        select dc;

            if (!String.IsNullOrEmpty(county))
            {
                query = query.Where(dc => dc.County.Contains(county));
            }

            if (!String.IsNullOrEmpty(state))
            {
                query = query.Where(dc => dc.State.Contains(state));
            }

            return query;
        }
    }
}
