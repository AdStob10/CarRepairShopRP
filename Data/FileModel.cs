using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairShopRP.Data
{

 

    public class FileModel
    {
        [Key]
        public int FileModelId { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        [StringLength(30, MinimumLength = 5)]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Upload file")]
        public string FileName { get; set; }


        [NotMapped]
        [Display(Name = "Upload file")]
        public IFormFile File { get; set; }

       
    }
}
