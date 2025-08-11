using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiRepositories.Contacts
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
        long TotalRecords { get; }
    }
}