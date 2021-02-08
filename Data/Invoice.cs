using CarRepairShopRP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{
    public class Invoice
    {
        public int InvoiceID { get; set; }


        public string InvoiceNumber { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "To pay")]
        public decimal Sum { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of issue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime createdDate { get; set; }



        public int RepairID { get; set; }
        public Repair Repair { get; set; }

        public RepairShopUser IssuedBy {get; set;}

        public RepairShopUser IssuedTo { get; set; }
    }
}
