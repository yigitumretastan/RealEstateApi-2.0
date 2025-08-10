using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApiCore.DTOs
{
    public class UpdateListingDto
    {
        public  string? Name { get; set; }
        public  string? Description { get; set; }
        public  string? Province { get; set; }
        public  string? District { get; set; }
        public  string? Street { get; set; }
        public  string? Apartment { get; set; }
        public  string? RoomCount { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "room size must be greater than 0")]
        public int? RoomSize { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "room size must be greater than 0")]
        public decimal? Price { get; set; }

    }
}