using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstateApiRepositories;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public abstract class ManagerBase<T, TKey> : IServiceBase<T, TKey> where T : class, IEntityEntryGraphIterator, new() where TKey : IEquatable<TKey>
    {
        protected readonly IPaginationUriService uriService;
        protected readonly IUserRepository<T, TKey> userRepository;
        protected readonly IPaymentRepository<T, TKey> paymentRepository;
        protected readonly IListingRepository<T, TKey> listingRepository;

        protected ManagerBase(IUserRepository<T, TKey> repository, IPaginationUriService uriService)
        {
            this.uriService = uriService;
            this.userRepository = userRepository;
            this.paymentRepository = paymentRepository;
            this.listingRepository = listingRepository;
        }


        public virtual async Task<IResult> GetAllAsync(Expression<Func<T, bool>>? predicate = null, PaginationQuery? paginationQuery = null)
        {
            var data = repository.Get(predicate, paginationQuery);
            if (data == null)
            {
                return new Result(HttpStatusCode.NotFound, "NotFound");
            }

            var list = await data.ToListAsync();
            var count = await repository.CountAsync();
            return PaginationExtensions.CreatePaginationResult(list, HttpStatusCode.OK, paginationQuery, count, uriService);
        }

        public virtual async Task<IResult> GetByIdAsync(TKey id)
        {
            var data = await repository.GetByIdAsync(id);
            return data == null ? new Result(HttpStatusCode.NotFound, "NotFound") : new DataResult<T>(data, HttpStatusCode.OK, 1);
        }
    }
}
}