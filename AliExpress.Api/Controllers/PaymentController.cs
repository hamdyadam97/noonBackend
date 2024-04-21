using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Order;
using AliExpress.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly IPaymentMethodService _paymentMethodService;
        public PaymentController(IPaymentMethodService paymentMethodService) 
        {
            _paymentMethodService = paymentMethodService;

        }

        [HttpPost]
        public async Task<ActionResult>CreatePaymentAsync(PaymentMethodDTO paymentMethodDTO)
        {
            if(ModelState.IsValid)
            {
                var pay=await _paymentMethodService.Create(paymentMethodDTO);
                if(pay != null)
                {
                    if (pay.Entity == null)
                        return BadRequest("Error in Creation");
                    else
                    {
                        var createdUrl = Url.Link("GetID", new { id = pay.Entity.Id });
                        return Created(createdUrl, pay.Entity);
                    }
                }
               
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult>Update(PaymentMethodDTO paymentMethodDTO)
        {
            if(ModelState.IsValid)
            {
                var pay=await _paymentMethodService.Update(paymentMethodDTO);
                if (pay == null)
                    return BadRequest("Error in Update Try Agin Later ");
                else
                {
                    var updateURL= Url.Link("GetID", new { id = pay.Entity.Id });
                     return Ok("Payment method updated successfully.");

                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var pay=await _paymentMethodService.GetOne(id);
            if(pay.Entity == null)
            {
                return NotFound($"Payment Method with ID {id} not found in the database");
            }
            else
            {
                var p = await _paymentMethodService.Delete(id);
                return Ok("Product Deleted sucessfully");
            }

        }

    }
}
