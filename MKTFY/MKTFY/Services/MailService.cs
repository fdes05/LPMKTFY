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

        public async Task<Response> SendEmail(string To, string From, string Subject, string PlainTextContent, string HtmlContent)
        {
            var apiKey = _configuration.GetSection("SendGrid").GetValue<string>("Key");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(From, "Admin");            
            var to = new EmailAddress(To, "Valued Customer");           
            var msg = MailHelper.CreateSingleEmail(from, to, Subject, PlainTextContent, HtmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
