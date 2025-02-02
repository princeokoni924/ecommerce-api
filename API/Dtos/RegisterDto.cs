using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
         public string FirstName { get; set; }= string.Empty;
         [Required]
        public string LastName { get; set; }= string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;
        [Required]
        [StringLength(9, ErrorMessage ="password should be at least to digit, and must contain symbol and alphanumeric")]
        public string Password { get; set; }= string.Empty;
        [Required]
        [Compare(nameof(Password),  ErrorMessage ="password doesn't match")]
        public string ConfirmPassword { get; set; }= string.Empty;   
    }
}