using SendGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class EmailVerificationResponseVM
    {
        public EmailVerificationResponseVM(Response response)
        {
            StatusCode = response.StatusCode.ToString();
            MessageBody = response.Body.ToString();
        }

        public string StatusCode { get; set; }

        public string MessageBody { get; set; }
    }
}
