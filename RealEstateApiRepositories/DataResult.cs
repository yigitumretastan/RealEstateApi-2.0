using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiRepositories
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, HttpStatusCode statusCode) : base(statusCode)
        {
            this.Data = data;
        }
        public DataResult(T data, HttpStatusCode statusCode, int totalRecords) :this(data, statusCode)
        {
            this.TotalRecords = totalRecords;
        }

        public DataResult(T data, HttpStatusCode statusCode, int totalRecords, string message) : base(statusCode)
        {
            this.Data = data;
            this.TotalRecords = totalRecords;
        }
        public DataResult(T data, HttpStatusCode statusCode, string message) : base(statusCode)
        {
            this.Data = data;
        }
        public T Data { get; }
        public int TotalRecords { get; }
    }
}