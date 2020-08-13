using DASIT.EmailServices.AspNet;
using DASIT.EmailServices.EmailAPI;
using DASIT.EmailServices.EmailAPI.AspNet;
using EmailFactoryNugetDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmailFactoryNugetDemo.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult EmailSent()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailViewModel model)
        {

            
            if (ModelState.IsValid)
            {


                var send = EmailServiceFactory.GetEmailSender();

                await send.SendEmailAsync(model.Email, model.Subject, model.Body);

               return RedirectToAction("EmailSent", "Home");

                /*
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)// || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                if (!(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                    MailMessage email = new MailMessage()
                    {
                        From = new MailAddress("KPMAppNoReply@oregon.gov"),
                        Subject = "Confirm your account",
                        Body = "Please confirm your account by clicking the lnik below." + Environment.NewLine + callbackUrl
                    };

                    email.To.Add(model.Email);

                    Email.SendEmailWithRetry(new SmtpClient(), email, 3);

                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                    MailMessage email = new MailMessage()
                    {
                        From = new MailAddress("KPMAppNoReply@oregon.gov"),
                        Subject = "Reset Password",
                        Body = "Please reset your password by clicking the link below." + Environment.NewLine + callbackUrl
                    };

                    email.To.Add(model.Email);

                    Email.SendEmailWithRetry(new SmtpClient(), email, 3);

                    //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }*/
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }
}