using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiRepositories
{
    public class Result : IResult
    {
        public Result(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }
        public Result(HttpStatusCode statusCode, string message) : this(statusCode)
        {
            Message = message;
        }
        public int StatusCode { get; }
        public string Message { get; set; } = string.Empty;
    }
}