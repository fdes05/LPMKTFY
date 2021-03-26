using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    /// <summary>
    /// Interface Mail Service
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Send email via SendGrid
        /// </summary>
        /// <param name="To"></param>
        /// <param name="From"></param>
        /// <param name="Subject"></param>
        /// <param name="PlainTextContent"></param>
        /// <param name="HtmlContent"></param>
        /// <returns></returns>
        public Task<Response> SendEmail(string To, string From, string Subject, string PlainTextContent, string HtmlContent);
    }
}
