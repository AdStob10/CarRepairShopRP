using CarRepairShopRP.Areas.Identity.Data;
using CarRepairShopRP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Pages.Invoices
{
    public class InvoiceViewModel
    {

        public int RepairID { get; set; }

        public string DocNum { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime issueDate { get; set; }

        [Display(Name = "Bill To")]
        public RepairShopUser BillUser { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "To Pay")]
        public decimal Sum { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Work cost")]
        public decimal WorkCost { get; set; }

        public ICollection<ReplacedPart> Parts { get; set; }

        [Display(Name = "Inovice issued by")]
        public string IssuedBy { get; set; }


    }
}
