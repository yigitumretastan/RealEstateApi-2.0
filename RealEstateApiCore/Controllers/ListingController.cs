using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;
using Microsoft.AspNetCore.Authorization;
using RealEstateApiCore.DTOs;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace RealEstateApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly IListingService listingService;
        public ListingController(IListingService listingService)
        {
            this.listingService = listingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllListing()
        {
            try
            {
                return Ok(await listingService.GetAllListingAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneListingById(int id)
        {
            try
            {
                var result = await listingService.GetListingByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateListing(CreateListingDto createListingDto)
        {
            try
            {
                if (createListingDto == null) return BadRequest();
                var listing = new Listing
                {
                    Name = createListingDto.Name,
                    Description = createListingDto.Description,
                    Province = createListingDto.Province,
                    District = createListingDto.District,
                    Street = createListingDto.Street,
                    Apartment = createListingDto.Apartment,
                    RoomCount = createListingDto.RoomCount,
                    RoomSize = createListingDto.RoomSize,
                    Price = createListingDto.Price
                };
                var CreateListing = await listingService.CreateListingAsync(listing);
                return Ok(CreateListing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Listing>> UpdateListing(int id, UpdateListingDto updateListingDto)
        {
            try
            {
                var existingListing = await listingService.GetListingByIdAsync(id);
                if (existingListing == null)
                    return NotFound();

                var listing = new Listing
                {
                    Id = id,
                    Name = updateListingDto.Name ?? existingListing.Name,
                    Description = updateListingDto.Description ?? existingListing.Description,
                    Province = updateListingDto.Province ?? existingListing.Province,
                    District = updateListingDto.District ?? existingListing.District,
                    Street = updateListingDto.Street ?? existingListing.Street,
                    Apartment = updateListingDto.Apartment ?? existingListing.Apartment,
                    RoomCount = updateListingDto.RoomCount ?? existingListing.RoomCount,
                    RoomSize = updateListingDto.RoomSize > 0 ? updateListingDto.RoomSize.Value : existingListing.RoomSize,
                    Price = updateListingDto.Price > 0 ? updateListingDto.Price.Value : existingListing.Price,
                };

                var updateListing = await listingService.UpdateListingAsync(id, listing);
                if (updateListing == null)
                    return BadRequest();

                return Ok(updateListing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Listing>> DeleteListing(int id)
        {
            try
            {
                var deleteListing = await listingService.DeleteListingAsync(id);
                if (deleteListing == null) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}