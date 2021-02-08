using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{

    public enum EngineFuel
    {
        Gasoline, Diesel
    }

    public enum BodyType
    {
        Sedan, Kombi, Hatchback
    }

    public class Car
    {
        [Key]
        public int CarID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Production Year")]
        public uint productionYear { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:####}")]
        [Display(Name = "Engine Capacity ( cm3 )")]
        [Column(TypeName="int")]
        [Range(800,3000)]
        public int EngineCapacity { get; set; }

        [Display(Name = "Type of Fuel")]
        public EngineFuel EngineFuel { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date of last oil change")]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime oilChangeDate { get; set; }

        [Display(Name = "Type of Body")]
        public BodyType BodyType { get; set; }


        public ICollection<Repair> Repairs { get; set; }
    }
}
