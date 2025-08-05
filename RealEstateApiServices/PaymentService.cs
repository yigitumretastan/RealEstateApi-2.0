using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public class PaymentService : IPaymentService
    {
        public Task<Payment> CreatePaymentAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Payment?> DeletePaymentAsync(int paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<Payment?> UpdatePaymentAsync(int paymentId, Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}