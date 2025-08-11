using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PaymentRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Payment>> GetAllPayment()
        {
            return await applicationDbContext.Payment.ToListAsync();
        }

        public async Task<Payment?> GetPaymentById(int paymentId)
        {
            return await applicationDbContext.Payment.FirstOrDefaultAsync(p => p.Id == paymentId);
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            var result = await applicationDbContext.Payment.AddAsync(payment);
            await applicationDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Payment?> UpdatePayment(int paymentId, Payment payment)
        {
            var result = await applicationDbContext.Payment.FirstOrDefaultAsync(p => p.Id == paymentId);
            if (result != null)
            {
                result.PaymentMethod = payment.PaymentMethod;
                result.CardName = payment.CardName;
                result.CardNumber = payment.CardNumber;
                result.CardCode = payment.CardCode;

                await applicationDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Payment?> DeletePayment(int paymentId)
        {
            var result = await applicationDbContext.Payment.FirstOrDefaultAsync(p => p.Id == paymentId);
            if (result != null)
            {
                applicationDbContext.Payment.Remove(result);
                await applicationDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<long> GetTotalCount()
        {
            return await applicationDbContext.Payment.CountAsync();
        }

        public async Task<IEnumerable<Payment>> GetPagedPayment(int pageNumber, int pageSize)
        {
            return await applicationDbContext.Payment
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
        }
    }
}