using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApiRepositories.Contacts
{
    public interface IResult
    {
        int StatusCode { get; }
        string Message { get; } 
    }
}