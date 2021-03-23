using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailFactoryNugetDemo.Models
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "To")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject:")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Body:")]
        public string Body { get; set; }
    }

}



