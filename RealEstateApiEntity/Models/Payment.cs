using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiEntity.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public required string PaymentMethod { get; set; }
        public required string CardName { get; set; }
        public required int CardNumber { get; set; }
        public required int CardCode { get; set; }
        public decimal Price { get; }
    }
}