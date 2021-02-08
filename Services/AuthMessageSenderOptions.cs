using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Services
{
    public class AuthMessageSenderOptions
    {
        public const string Sender = "SendGrid";
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
