using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiRepositories.Contacts
{
    public interface IPaginationResult<T> : IResult
    {
        IReadOnlyList<T> Data { get; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        Uri? FirstPage { get; }
        Uri? LastPage { get; }
        int TotalPages { get; }
        int TotalRecords { get; }
        Uri? NextPage { get; }
        Uri? PreviousPage { get; }
    }
}