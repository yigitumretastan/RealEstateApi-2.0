using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;
using RealEstateApiServices.Contacts;
using Microsoft.AspNetCore.Authorization;
using RealEstateApiCore.DTOs;
using RealEstateApiRepositories;
using RealEstateApiServices;

namespace RealEstateApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaginationUriService paginationUriService;
        private readonly IPaymentService paymentService;
        private readonly IListingService listingService;
        public PaymentController(IPaymentService paymentService, IListingService listingService, IPaginationUriService paginationUriService)
        {
            this.paymentService = paymentService;
            this.listingService = listingService;
            this.paginationUriService = paginationUriService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPayment([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var paginationQuery = new PaginationQuery(pageNumber, pageSize);
                var totalRecords = await paymentService.GetTotalCountAsync();
                var pagedData = await paymentService.GetPagedPaymentAsync(pageNumber, pageSize);
                var paginationResult = PaginationExtensions.CreatePaginationResult(
                    pagedData.ToList(),
                    System.Net.HttpStatusCode.OK,
                    paginationQuery,
                    totalRecords,
                    paginationUriService);
                return Ok(paginationResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            try
            {
                var result = await paymentService.GetPaymentByIdAsync(id);
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
        public async Task<ActionResult<Payment>> CreatePayment(PaymentDto paymentDto)
        {
            try
            {
                if (paymentDto == null)
                {
                    return BadRequest();
                }
                var payment = new Payment
                {
                    ListingId = paymentDto.ListingId,
                    PaymentMethod = paymentDto.PaymentMethod,
                    CardName = paymentDto.CardName,
                    CardNumber = paymentDto.CardNumber,
                    CardCode = paymentDto.CardCode,
                };
                var createdPayment = await paymentService.CreatePaymentAsync(payment);

                return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /*   
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            try
            {
                if (payment == null || id != payment.Id)
                {
                    return BadRequest();
                }

                var updatedPayment = await paymentService.UpdatePaymentAsync(id, payment);

                if (updatedPayment == null) return NotFound();

                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        */
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            try
            {
                var deletedPayment = await paymentService.DeletePaymentAsync(id);
                if (deletedPayment == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
