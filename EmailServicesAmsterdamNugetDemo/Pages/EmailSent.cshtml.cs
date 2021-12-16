using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DASIT.EmailServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmailServicesContainedNugetDemo.Pages
{
    public class EmailSentModel : PageModel
    {

      

        public EmailSentModel()
        {
           
        }

       

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

       
    }
}
