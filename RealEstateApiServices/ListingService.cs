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

        public IEnumerable<Listing> GetFilterListing(
            string? name = null,
            string? Province = null,
            string? District = null,
            string? Street = null,
            string? Apartment = null,
            int? RoomCount = null,
            int? RoomSize = null,
            decimal? Price = null)
        {
            var listing = listingRepository.GetAllListing();
            if (!string.IsNullOrEmpty(name))
                listing = listing.Where(x => x.Name == name);
            if (!string.IsNullOrEmpty(Province))
                listing = listing.Where(x => x.Province == Province);
            if (!string.IsNullOrEmpty(District))
                listing = listing.Where(x => x.District == District);
            if (!string.IsNullOrEmpty(Street))
                listing = listing.Where(x => x.Street == Street)
            if (!string.IsNullOrEmpty(Apartment))
                listing = listing.Where(x => x.Apartment == Apartment);
            if (!RoomCount.HasValue)
                listing = listing.Where(x => x.RoomCount == RoomCount);
            if (!RoomSize.HasValue)
                listing = listing.Where(x => x.RoomSize == RoomSize);
            if (!Price.HasValue)
                listing = listing.Where(x => x.Price == Price);
            return listing;
        }

        public async Task<IEnumerable<Listing>> GetAllListingAsync()
        {
            // return await listingRepository.GetAllListing();
            return await ListingService.GetFilterListing();
        }

        public async Task<Listing?> GetListingByIdAsync(int listingId)
        {
            return await listingRepository.GetListingById(listingId);
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            if (listing == null)
                throw new ArgumentNullException(nameof(listing));
            if (listing.Price <= 0)
            {
                throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(listing.Price));
            }
            return await listingRepository.CreateListing(listing);
        }

        public async Task<Listing?> UpdateListingAsync(int id, Listing listing)
        {
            if (listing == null)
                throw new ArgumentNullException(nameof(listing));
            if (listing.Price <= 0)
            {
                throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(listing.Price));
            }

            return await listingRepository.UpdateListing(id, listing);
        }

        public async Task<Listing?> DeleteListingAsync(int listingId)
        {
            return await listingRepository.DeleteListing(listingId);
        }
    }
}