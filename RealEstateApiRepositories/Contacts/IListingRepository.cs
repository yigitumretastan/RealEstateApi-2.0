using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;

namespace RealEstateApiRepositories.Contacts
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAllListing();
        Task<Listing?> GetListingById(int listingId);
        Task<Listing> CreateListing(Listing listing);
        Task<Listing?> UpdateListing(int id, Listing listing);
        Task<Listing?> DeleteListing(int listingId);
    }
}