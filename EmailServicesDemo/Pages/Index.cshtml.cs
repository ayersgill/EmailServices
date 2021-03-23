using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DASIT.EmailServices;
using DASIT.EmailServices.AspNetCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace EmailServicesDemo.Pages
{

    public class IndexModel : PageModel
    {

        private readonly IEmailService _emailService;

        public IndexModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();


        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "To:")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Subject:")]
            public string Subject{ get; set; }

            [Required]
            [Display(Name = "Body:")]
            public string Body { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {

                try
                {

                    await _emailService.SendHtmlEmailAsync(Input.Email, Input.Subject, Input.Body);
                } catch (EmailSenderException ex)
                {
                    Log.Error(ex, "Email Exception");
                }

                return RedirectToPage("./EmailSent");

            }

                return Page();

        }
    }
}
