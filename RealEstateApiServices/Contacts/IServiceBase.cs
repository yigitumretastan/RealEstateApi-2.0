using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstateApiRepositories;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiServices.Contacts
{
    public interface IServiceBase<T, in TKey> where T : class, IEntityEntryGraphIterator, new() where TKey : IEquatable<TKey>
    {
        Task<IResult> GetAllAsync(Expression<Func<T, bool>>? predicate = null, PaginationQuery? paginationQuery = null);
        Task<IResult> GetByIdAsync(TKey id);
    }
}