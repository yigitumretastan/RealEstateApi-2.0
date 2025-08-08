using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApiCore.DTOs
{
    public class CreateListingDto
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Province { get; set; }
        [Required]
        public required string District { get; set; }
        [Required]
        public required string Street { get; set; }
        [Required]
        public required string Apartment { get; set; }
        [Required]
        public required string RoomCount { get; set; }
        [Required]
        public int RoomSize { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}