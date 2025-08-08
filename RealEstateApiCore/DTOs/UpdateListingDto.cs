using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public  string? RoomSize { get; set; }
        public decimal Price { get; set; }

    }
}