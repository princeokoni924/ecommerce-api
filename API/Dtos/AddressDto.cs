using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AddressDto
    {
        [Required]
        public  string Street { get; set; } = string.Empty;

        [Required]
        public string LandMark{ get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        public string? PostalCode { get; set; }
        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string NearestBustop { get; set; } = string.Empty;
    }
}