using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiEntity.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string? Description { get; set; }
        public required string Province { get; set; }
        public required string District { get; set; }
        public required string Street { get; set; }
        public required string Apartment { get; set; }
        public required string RoomCount { get; set; }
        public required string RoomSize { get; set; }
        public decimal Price { get; set; }
    }
}