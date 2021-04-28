using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Exceptions;
using MKTFY.Models.Entities;
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
        private readonly UserManager<User> _userManager;

        public PaymentService(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        public async Task<Customer> CreateStripeCustomer(RegisterVM data)
        {
            StripeConfiguration.ApiKey = "sk_test_51IW6QiLk43ipxPsRBKVoI1zb9UfHmn5WMBzd4ceYgidrkZCS9mAk42HiBUfSEJizVHJhGPSxkPfgGI261qroymwa00JAejLKQx";

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
            StripeConfiguration.ApiKey = "sk_test_51IW6QiLk43ipxPsRBKVoI1zb9UfHmn5WMBzd4ceYgidrkZCS9mAk42HiBUfSEJizVHJhGPSxkPfgGI261qroymwa00JAejLKQx";

            var user = await _userService.GetUserById(userId);
            var stripeCustomerId = user.StripeCustomerId;

            var options = new SetupIntentCreateOptions
            {
                Customer =  stripeCustomerId,
                PaymentMethodTypes = new List<string> {
                    "card"
                    },
                Usage = "on_session"
            };
            var service = new SetupIntentService();
            var intent = service.Create(options);

            return intent.ClientSecret;
        }

        public async Task<User> SavePaymentMethodId(string userId, string paymentMethodId)
        {
            var user = await _userService.GetUserById(userId);
            user.StripePaymentMethodId = paymentMethodId;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {                
                return user;
            }
            else
            {
                throw new UserNotSavedException("Something went wrong and user wasn't saved. Please try again.");
            }
        }
    }
}
