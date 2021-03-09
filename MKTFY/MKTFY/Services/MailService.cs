using Microsoft.Extensions.Configuration;
using MKTFY.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class MailService : IMailService

    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration config)
        {
            _configuration = config;
        }

        public async Task<Response> SendEmail(string toEmail, string fromEmail, string subject, string plainTextContent, string htmlContent)
        {
            var apiKey = _configuration.GetSection("SendGrid").GetValue<string>("Key");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, "Admin");            
            var to = new EmailAddress(toEmail, "Valued Customer");           
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
