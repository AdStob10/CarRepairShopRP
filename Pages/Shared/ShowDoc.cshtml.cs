using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRepairShopRP.Pages.Shared
{
    public class ShowDocModel : PageModel
    {

        public ActionResult OnGet(string fname)
        {
            return File("/bills/"+ fname, MediaTypeNames.Application.Octet,
                fname);
        }
    }
}
