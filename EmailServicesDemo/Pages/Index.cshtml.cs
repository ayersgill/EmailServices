using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmailServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

                await _emailService.SendEmailAsync(Input.Email, Input.Subject, Input.Body);

                return RedirectToPage("./EmailSent");

            }

                return Page();

        }
    }
}
