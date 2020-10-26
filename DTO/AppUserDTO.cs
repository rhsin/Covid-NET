using Covid.Models;
using System;
using System.Collections.Generic;

namespace Covid.DTO
{
    public class AppUserDTO
    {
        public string Id { get; set; }
        public int? AccountId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string County { get; set; }
        public string Role { get; set; }

        public ICollection<CountList> CountLists { get; set; }
    }
}
