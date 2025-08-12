using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiServices.Contacts
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetFilterListing(
            string? name = null,
            string? province = null,
            string? district = null,
            string? street = null,
            string? apartment = null,
            string? roomCount = null,
            int? roomSize = null,
            decimal? price = null,
            int pageNumber = 1,
            int pageSize = 10,
            string? sortBy = null,
            string? sortOrder = null);
        IQueryable<Listing> GetAllListing();
        Task<Listing?> GetListingByIdAsync(int listingId);
        Task<Listing> CreateListingAsync(Listing listing);
        Task<Listing?> UpdateListingAsync(int ListingId, Listing listing);
        Task<Listing?> DeleteListingAsync(int listingId);
        Task<int> GetTotalCountAsync();

    }
}