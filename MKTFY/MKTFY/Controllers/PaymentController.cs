using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public async Task CreateConnectedAccount([FromBody] ConnectedAccountAddVM data)
        {
            // create an express account in the PaymentService class

            // MIGHT BE OPTIONAL: do account link with 'redirect link' in PaymentService class

            // Connect Onboarding require 'return url' and 'refresh url'
        }

       // public async Task<ClientSecret> CreatePaymentIntent()
       // {
            // creates payment intent through Stripe API and receives a ClientSecret that needs to be returned to FrontEnd
      //  }

        //[HttpPost("webhook")]
        //public async Task<IActionResult> ProcessWebhookEvent()
        //{
        //    // This is to receive the PaymentCompleted event and then process the ConnectedAccount integration to
        //    // transfer money to the Seller
        //}
    }
}
