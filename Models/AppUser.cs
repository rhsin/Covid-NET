using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Covid.Models
{
    public class AppUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
