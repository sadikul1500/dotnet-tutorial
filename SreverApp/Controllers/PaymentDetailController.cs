using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        //Get: api/PaymentDetail 
        //[AllowAnonymous]
        //[HttpGet("{rooms}")]
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            //return new String [] {"this", "is"};
            return await _context.PaymentDetails.ToListAsync();
        }

        [HttpGet]

        public ActionResult<IEnumerable<String>> getString()
        {
            return new String[] {"this", "is"};
        }

        //POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.Id }, paymentDetail);
        }

        //PUT: api/PaymentDetail/5
        [HttpPut("{Id}")]
        public async Task<ActionResult<PaymentDetail>>  PutPaymentDetail(int Id, PaymentDetail paymentDetail)
        {
            if (Id != paymentDetail.Id)
            {
                return BadRequest();

            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException)
            {
                if(!PaymentDetailExists(Id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/PaymentDetail/5
        [HttpDelete]
        public async Task<IActionResult> DeletePaymentDetail(int Id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(Id);
            if(paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int Id)
        {
            return _context.PaymentDetails.Any(e => e.Id == Id);
        }

    }
}
