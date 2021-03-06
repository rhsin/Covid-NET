﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class AppUser : IdentityUser
    {
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string County { get; set; }
        public string Role { get; set; }

        public ICollection<CountList> CountLists { get; set; }
    }
}
