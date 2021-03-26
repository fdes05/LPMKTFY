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
    /// <summary>
    /// Mail Service
    /// </summary>
    public class MailService : IMailService

    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Mail Service Constructor which takes in an IConfiguration)
        /// </summary>
        /// <param name="config"></param>
        public MailService(IConfiguration config)
        {
            _configuration = config;
        }
        /// <summary>
        /// Send Email Method
        /// </summary>
        /// <param name="toEmail">Requires 'To email' address</param>
        /// <param name="fromEmail">Requires 'From email' address</param>
        /// <param name="subject">Requires a subject line</param>
        /// <param name="plainTextContent">Requires plain text that email needs to contain</param>
        /// <param name="htmlContent">Requires HTML content that email needs to contain</param>
        /// <returns></returns>
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
