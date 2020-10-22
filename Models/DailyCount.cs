using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Models
{
    public class DailyCount
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public int Fips { get; set; }
        public int Cases { get; set; }
        public int Deaths { get; set; }
    }
}
