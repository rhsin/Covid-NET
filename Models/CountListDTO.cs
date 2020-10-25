using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class CountListDTO
    {
        public int Id { get; set; }

        public AppUserDTO AppUserDTO { get; set; }

        public IList<CountListDailyCount> CountListDailyCounts { get; set; }
    }
}
