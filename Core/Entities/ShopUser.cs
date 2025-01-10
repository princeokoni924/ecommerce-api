using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ShopUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Address? Address{ get; set; }
    }
}