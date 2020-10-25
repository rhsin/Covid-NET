using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class DailyCount
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public float? Fips { get; set; }
        public int? Cases { get; set; }
        public int? Deaths { get; set; }

        public IList<CountListDailyCount> CountListDailyCounts { get; set; }
    }
}
