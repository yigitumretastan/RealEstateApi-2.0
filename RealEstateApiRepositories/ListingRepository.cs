using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiRepositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ListingRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        
        public async Task<IEnumerable<Listing>> GetAllListing()
        {
            return await applicationDbContext.Listing.ToListAsync();
        }

        public async Task<Listing?> GetListingById(int listingId)
        {
            var result = await applicationDbContext.Listing.FirstOrDefaultAsync(l => l.Id == listingId);
            return result;
        }

        public async Task<Listing> CreateListing(Listing listing)
        {
            var result = await applicationDbContext.Listing.AddAsync(listing);
            await applicationDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Listing?> UpdateListing(int id, Listing listing)
        {
            var result = await applicationDbContext.Listing.FirstOrDefaultAsync(l => l.Id == id);
            if (result != null)
            {
                result.Name = listing.Name;
                result.Description = listing.Description;
                result.Province = listing.Province;
                result.District = listing.District;
                result.Street = listing.Street;
                result.Apartment = listing.Apartment;
                result.RoomCount = listing.RoomCount;
                result.RoomSize = listing.RoomSize;
                result.Price = listing.Price;

                await applicationDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        
        public async Task<Listing?> DeleteListing(int listingId)
        {
            var result = await applicationDbContext.Listing.FirstOrDefaultAsync(l => l.Id == listingId);
            if (result != null)
            {
                applicationDbContext.Listing.Remove(result);
                await applicationDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}