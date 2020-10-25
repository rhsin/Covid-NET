using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class CountListDailyCount
    {
        public int CountListId { get; set; }
        public CountList CountList { get; set; }

        public int DailyCountId { get; set; }
        public DailyCount DailyCount { get; set; }
    }
}
