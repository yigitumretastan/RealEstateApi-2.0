using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiEntity.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public required string PaymentMethot { get; set; }
        public int Biling { get; set; }
    }
}