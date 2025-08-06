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
        public async Task<IActionResult> CreateListing(Listing listing)
        {
            try
            {
                if (listing == null) return BadRequest();
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
        public async Task<ActionResult<Listing>> UpdateOneListing(int id, Listing listing)
        {
            try
            {
                var updateListing = await listingService.UpdateListingAsync(id, listing);
                if (updateListing == null) return BadRequest();
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
                return Ok(deleteListing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}