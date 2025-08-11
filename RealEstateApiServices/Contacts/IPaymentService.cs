using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiServices.Contacts
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentAsync();
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment?> UpdatePaymentAsync(int paymentId, Payment payment);
        Task<Payment?> DeletePaymentAsync(int paymentId);
        Task<long> GetTotalCountAsync();
        Task<IEnumerable<Payment>> GetPagedPaymentAsync(int pageNumber, int pageSize);
    }
}