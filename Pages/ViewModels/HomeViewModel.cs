using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Pages.ViewModels
{
    public class HomeViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int visits { get; set; }

        public string role { get; set; }

        public int repairs { get; set; }
    }
}
