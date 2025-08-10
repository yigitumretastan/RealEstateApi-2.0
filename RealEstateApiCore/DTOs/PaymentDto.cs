using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApiCore.DTOs
{
    public class PaymentDto
    {
        [Required]
        public int ListingId { get; set; }
        [Required]
        public required string PaymentMethod { get; set; }
        [Required]
        public required string CardName { get; set; }
        [Required]
        public long CardNumber { get; set; }
        [Required]
        public int CardCode { get; set; }
    }
}