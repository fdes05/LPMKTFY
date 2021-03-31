using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels;
using MKTFY.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUserService _userService;

        public PaymentService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Customer> CreateStripeCustomer(RegisterVM data)
        {
            StripeConfiguration.ApiKey = "pk_test_51IW6QiLk43ipxPsRdgDN7ILywr30zGCGKPnpAbAtX5ASbsGpvBNe0PWXZp5u4Jr3QtQJOL6xX29mtHQWpI5zj89J00nuV5k8ba";

            var options = new CustomerCreateOptions
            {
                Name = data.FirstName + data.LastName,
                Email = data.Email,
                Description = "Test Customer"                
            };
            var service = new CustomerService();
            var customer = service.Create(options);

            return customer;
        }

        public async Task<string> CreateStripeSetupIntent(string userId)
        {
            var user = await _userService.GetUserById(userId);
            var stripeCustomerId = user.StripeCustomerId;

            var options = new SetupIntentCreateOptions
            {
                Customer = "{stripeCustomerId}",
            };
            var service = new SetupIntentService();
            var intent = service.Create(options);

            return intent.ClientSecret;
        }
    }
}
