using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        private readonly IListingRepository listingRepository;

        public PaymentService(IPaymentRepository paymentRepository, IListingRepository listingRepository)
        {
            this.paymentRepository = paymentRepository;
            this.listingRepository = listingRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            return await paymentRepository.GetAllPayment();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await paymentRepository.GetPaymentById(paymentId);
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            var existingListing = await listingRepository.GetListingById(payment.ListingId);

            if (existingListing == null)
                throw new ArgumentException("Invalid listing ID", nameof(payment.ListingId));

            payment.Price = existingListing.Price;
            var createdPayment = await paymentRepository.CreatePayment(payment);

            return createdPayment;
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

        public async Task<Payment?> DeletePaymentAsync(int paymentId)
        {
            return await paymentRepository.DeletePayment(paymentId);
        }

        public async Task<long> GetTotalCountAsync()
        {
            return await paymentRepository.GetTotalCount();
        }

        public async Task<IEnumerable<Payment>> GetPagedPaymentAsync(int pageNumber, int pageSize)
        {
            return await paymentRepository.GetPagedPayment(pageNumber, pageSize);
        }
    }
}