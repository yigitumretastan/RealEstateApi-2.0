using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiServices.Contacts
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListingAsync();
        Task<Listing?> GetListingByIdAsync(int listingId);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<Listing?> UpdateListingAsync(int ListingId, Listing listing);
        Task<Listing?> DeleteListingAsync(int listingId);
    }
}