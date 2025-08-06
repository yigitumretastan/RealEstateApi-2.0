using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiEntity.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public required string PaymentMethod { get; set; }
        public decimal Price { get; set; }
    }
}