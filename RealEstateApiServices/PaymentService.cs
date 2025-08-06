using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            return await paymentRepository.CreatePayment(payment);
        }

        public async Task<Payment?> DeletePaymentAsync(int paymentId)
        {
            return await paymentRepository.DeletePayment(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            return await paymentRepository.GetAllPayment();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await paymentRepository.GetPaymentById(paymentId);
        }

        public async Task<Payment?> UpdatePaymentAsync(int paymentId, Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            
            var existingPayment = await paymentRepository.GetPaymentById(paymentId);
            if (existingPayment == null)
                return null;

            if (paymentId != payment.Id)
                throw new ArgumentException("Payment ID mismatch");
                
            return await paymentRepository.UpdatePayment(paymentId, payment);
        }
    }
}