using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RealEstateApiEntity.Models;
using RealEstateApiRepositories.Contacts;

namespace RealEstateApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            try
            {
                var payments = await paymentRepository.GetAllPayment();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            try
            {
                var result = await paymentRepository.GetPaymentById(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest();
                }
                var createdPayment = await paymentRepository.CreatePayment(payment);

                return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            try
            {
                if (payment == null || id != payment.Id)
                {
                    return BadRequest();
                }

                var updatedPayment = await paymentRepository.UpdatePayment(id, payment);

                if (updatedPayment == null) return NotFound();

                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            try
            {
                var deletedPayment = await paymentRepository.DeletePayment(id);
                if (deletedPayment == null)
                {
                    return NotFound();
                }

                return Ok(deletedPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
