using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "product name must be specify")]
        public string Name { get; set; }= string.Empty;

        [Required(ErrorMessage = "give description about the product ")]
        public string Description { get; set; }=string.Empty;

        [Required]
        public string ProductType { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; }= string.Empty;

        [Range(0.1, double.MaxValue, ErrorMessage = "price must be at least greater 0")]
        public double Price { get; set; }

        [Required]
        public string PictureUrl { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "quantity in stock must be at least 1!")]
        public int QtyInStk { get; set; }
    }
}