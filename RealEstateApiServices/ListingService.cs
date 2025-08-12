using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RealEstateApiServices
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }
        public async Task<IEnumerable<Listing>> GetFilterListing(
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
            string? sortOrder = null)
        {
            IQueryable<Listing> query = listingRepository.GetAllListing();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name == name);
            if (!string.IsNullOrEmpty(province))
                query = query.Where(x => x.Province == province);
            if (!string.IsNullOrEmpty(district))
                query = query.Where(x => x.District == district);
            if (!string.IsNullOrEmpty(street))
                query = query.Where(x => x.Street == street);
            if (!string.IsNullOrEmpty(apartment))
                query = query.Where(x => x.Apartment == apartment);
            if (!string.IsNullOrEmpty(roomCount))
                query = query.Where(x => x.RoomCount == roomCount);
            if (roomSize.HasValue)
                query = query.Where(x => x.RoomSize == roomSize);
            if (price.HasValue)
                query = query.Where(x => x.Price == price);

            bool descending = sortOrder?.ToLower() == "desc";
            if (sortBy?.ToLower() == "price")
            {
                query = descending ? query.OrderByDescending(l => l.Price) : query.OrderBy(l => l.Price);
            }
            else
            {
                query = query.OrderBy(l => l.Name);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        /*
        public async Task<int> GetFilteredCountAsync(
                   string? name = null,
                   string? province = null,
                   string? district = null,
                   string? street = null,
                   string? apartment = null,
                   string? roomCount = null,
                   int? roomSize = null,
                   decimal? price = null)
        {
            IQueryable<Listing> query = listingRepository.GetAllListing();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name == name);
            if (!string.IsNullOrEmpty(province))
                query = query.Where(x => x.Province == province);
            if (!string.IsNullOrEmpty(district))
                query = query.Where(x => x.District == district);
            if (!string.IsNullOrEmpty(street))
                query = query.Where(x => x.Street == street);
            if (!string.IsNullOrEmpty(apartment))
                query = query.Where(x => x.Apartment == apartment);
            if (!string.IsNullOrEmpty(roomCount))
                query = query.Where(x => x.RoomCount == roomCount);
            if (roomSize.HasValue)
                query = query.Where(x => x.RoomSize == roomSize);
            if (price.HasValue)
                query = query.Where(x => x.Price == price);

            return await query.CountAsync();
        }
        */
        /*
        public async Task<IEnumerable<Listing>> GetAllListingAsync()
        {

            return await listingRepository.GetAllListing();
            //return await GetFilterListing();
        }
        */
        public IQueryable<Listing> GetAllListing()
        {
            return listingRepository.GetAllListing();
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

        public async Task<int> GetTotalCountAsync()
        {
            return await listingRepository.GetTotalCount();
        }

    }
}