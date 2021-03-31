using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Customer> CreateStripeCustomer(RegisterVM data);
        Task<ActionResult> CreateStripeSetupIntent(string userId);
    }
}
