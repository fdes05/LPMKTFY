using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class ConnectedAccountAddVM
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Capabilities { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string ExternalAccounts { get; set; }
        public bool PayoutsEnabled { get; set; }


    }
}
