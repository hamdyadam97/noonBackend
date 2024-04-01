using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDTO transactionDTO)

        {
            if(ModelState.IsValid)
            {
                var trran = await _transactionService.Create(transactionDTO);
                if (trran != null)
                {
                    if(trran.Entity==null)
                        return BadRequest("Error in Creation");
                    else
                    {
                        var createdUrl = Url.Link("GetID", new { id = trran.Entity.Id });
                        return Created(createdUrl, trran);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateTransaction(TransactionDTO transactionDTO)
        {
            if (ModelState.IsValid)
            {
                var tar=await _transactionService.Update(transactionDTO);
                if (tar == null)
                {
                    return BadRequest("Error in Update Try Agin Later ");
                }
                else
                {
                    //var updateURL = Url.Link("GetID", new { id = pay.Entity.Id });
                    return Ok("Payment method updated successfully.");
                }
            }
            return BadRequest(ModelState);  
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult>DeleteTransaction(int id)
        {
            var tar = await _transactionService.GetOne(id);
            if (tar == null)
            {
                return NotFound($"Payment Method with ID {id} not found in the database");
            }
            else
            {
                var tar2 = await _transactionService.Delete(id);
                return Ok("Transaction Deleyed Successfully");
            }
        }



    }
}
