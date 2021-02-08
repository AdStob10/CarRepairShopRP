using CarRepairShopRP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Pages.Repairs
{
    public class RepairIndexData
    {
        public PaginatedList<Repair> Repairs { get; set; }
        public IEnumerable<ReplacedPart> ReplacedParts { get; set; }

        public bool blockNewParts { get; set; }

    }
}
