using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Contract;
using Core.Contract.IGeneric;
using Core.Entities;
using Core.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    public class PaymentController(IFlutterWavePaymentService _paymentService,
    IGenericRepo<DeliveryMethod> _dmRepo ) : BaseApiController
    {
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>>CreateOrUpdatePaymentIntent(string cartId){
           var cart = await _paymentService.InitializePayment(cartId);
           if(cart != null){
               return Ok(cart);
           }else{
            return BadRequest("Error in creating or updating payment intent");
           }
        }

        [HttpGet("getDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods(){
            return Ok(await _dmRepo.ListAllDataAsync());
        }
    }

  
}