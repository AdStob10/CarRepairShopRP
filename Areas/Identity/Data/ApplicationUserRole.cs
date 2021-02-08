using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Areas.Identity.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual RepairShopUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
