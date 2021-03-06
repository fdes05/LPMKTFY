using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IMailService
    {
        public Task<Response> SendEmail(string To, string From, string Subject, string PlainTextContent, string HtmlContent);
    }
}
