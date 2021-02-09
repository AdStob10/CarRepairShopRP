using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{
    public class ReplacedPart
    {
        public int ReplacedPartID { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Manufacturer { get; set; }


        [Range(1,500)]
        public int Quantity { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Production Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        public decimal Price { get; set; }


        public int RepairID { get; set; }
        public Repair Repair { get; set; }

        [Display(Name = "Old Part")]
        public FileModel OldPartImage { get; set; }

        [Display(Name = "Bill for New Part")]
        public FileModel NewPartBill { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
