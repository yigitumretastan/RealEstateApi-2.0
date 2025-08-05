using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiServices.Contacts;

namespace RealEstateApiServices
{
    public class ListingService : IListingService
    {
        public Task<Listing> CreateListingAsync(Listing listing)
        {
            throw new NotImplementedException();
        }

        public Task<Listing?> DeleteListingAsync(int listingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Listing>> GetAllListingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Listing?> GetListingByIdAsync(int listingId)
        {
            throw new NotImplementedException();
        }

        public Task<Listing?> UpdateListingAsync(int ListingId, Listing listing)
        {
            throw new NotImplementedException();
        }
    }
}