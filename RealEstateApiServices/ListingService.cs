using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;
using System.Text.RegularExpressions;

namespace RealEstateApiServices
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<IEnumerable<Listing>> GetAllListingAsync()
        {
            return await listingRepository.GetAllListing();
        }

        public async Task<Listing?> GetListingByIdAsync(int listingId)
        {
            return await listingRepository.GetListingById(listingId);
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            if (listing == null)
                throw new ArgumentNullException(nameof(listing));
            var pricePattern = @"^[1-9]\d*$";
            if (!Regex.IsMatch(listing.Price.ToString(), pricePattern))
            {
                throw new ArgumentException("The number entered cannot be 0 or start with 0.", nameof(listing.Price));
            }
            return await listingRepository.CreateListing(listing);
        }

        public async Task<Listing?> UpdateListingAsync(int id, Listing listing)
        {
            if (listing == null)
                throw new ArgumentNullException(nameof(listing));
            var pricePattern = @"^[1-9]\d*$";
            if (!Regex.IsMatch(listing.Price.ToString(), pricePattern))
            {
                throw new ArgumentException("The number entered cannot be 0 or start with 0.", nameof(listing.Price));
            }

            return await listingRepository.UpdateListing(id, listing);
        }

        public async Task<Listing?> DeleteListingAsync(int listingId)
        {
            return await listingRepository.DeleteListing(listingId);
        }
    }
}