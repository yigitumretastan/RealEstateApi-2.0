using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly IListingRepository listingRepository;
        public ListingController(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllListing()
        {
            try
            {
                return Ok(await listingRepository.GetAllListing());
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
                var result = await listingRepository.GetListingById(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateListing(Listing listing)
        {
            try
            {
                if (listing == null) return BadRequest();
                var CreateListing = await listingRepository.CreateListing(listing);
                return Ok(CreateListing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Listing>> UpdateOneListing(int id, Listing listing)
        {
            try
            {
                var updateListing = await listingRepository.UpdateListing(id, listing);
                if (updateListing == null) return BadRequest();
                return Ok(updateListing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Listing>> DeleteListing(int id)
        {
            try
            {
                var deleteListing = await listingRepository.DeleteListing(id);
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