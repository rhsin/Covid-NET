using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class CountList
    {
        public int Id { get; set; }

        public AppUser AppUser { get; set; }

        public IList<CountListDailyCount> CountListDailyCounts { get; set; }
    }
}
