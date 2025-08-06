using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiCore.Controllers.DTOs
{
    public class CreateListingDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Province { get; set; }
        public required string District { get; set; }
        public required string Street { get; set; }
        public required string Apartment { get; set; }
        public required string RoomCount { get; set; }
        public required string RoomSize { get; set; }
        public decimal Price { get; set; }
    }
}