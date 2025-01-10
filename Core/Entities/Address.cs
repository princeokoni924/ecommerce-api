using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Address: BaseEntity
    {
        public required string Street { get; set; }
        public required string LandMark{ get; set; }
        public required string City { get; set; } 
        public required string State { get; set; } 
        public string? PostalCode { get; set; }
        public required string Country { get; set; }
        public string NearestBustop { get; set; } = string.Empty;
    }
}