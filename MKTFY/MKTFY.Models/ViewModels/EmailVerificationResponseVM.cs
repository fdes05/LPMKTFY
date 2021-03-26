using SendGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    /// <summary>
    /// Email Verification Response View Model
    /// </summary>
    public class EmailVerificationResponseVM
    {
        /// <summary>
        /// EmailVerificationResponseVM Constructor which takes in a Response object)
        /// </summary>
        /// <param name="response"></param>
        public EmailVerificationResponseVM(Response response)
        {
            StatusCode = response.StatusCode.ToString();
            MessageBody = response.Body.ToString();
        }
        /// <summary>
        /// Status Code Field
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Message Body Field
        /// </summary>
        public string MessageBody { get; set; }
    }
}
