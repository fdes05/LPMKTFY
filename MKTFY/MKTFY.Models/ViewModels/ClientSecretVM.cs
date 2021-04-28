using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ClientSecretVM
    {
        public ClientSecretVM(string clientSecret)
        {
            ClientSecret = clientSecret;
        }

        public string ClientSecret { get; set; }
    }
}
