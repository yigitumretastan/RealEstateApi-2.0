using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiRepositories.Contacts
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPayment();
        Task<Payment?> GetPaymentById(int paymentId);
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment?> UpdatePayment(int paymentId, Payment payment);
        Task<Payment?> DeletePayment(int paymentId);
        Task<long> GetTotalCount();
        Task<IEnumerable<Payment>> GetPagedPayment(int pageNumber, int pageSize);
    }
}