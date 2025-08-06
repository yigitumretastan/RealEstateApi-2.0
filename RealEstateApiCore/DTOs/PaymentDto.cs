using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiCore.DTOs
{
    public class PaymentDto
    {
        public required string PaymentMethot { get; set; }
        public int Price { get; set; }
    }
}