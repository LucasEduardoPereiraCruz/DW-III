using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VasosInteligentes.Models;

namespace VasosInteligentes.Controllers
{
    public class AccountsController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private EmailService _emailService;
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, _emailService emailService)
        { 
            _userManager = userManager;
            _signInManager = SignInManager;
            EmailService = emailService; 
        }


        // GET
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(
            [Required][EmailAddress] string email,
            [Required] string password)
        {
            Console.WriteLine($"email:{email} - senha: {password}");
            if (ModelState.IsValid)
            {
                ApplicationUser appuser = await _userManager.FindByEmailAsync(email);
                if(appuser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(appuser, password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction ("Index", "Home");
                    }
                    ModelState.AddModelError(nameof(email), "Verifique as credenciais");
                }
            }
            return View();
        } // login
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //get 
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Informe o e-mail");
                return View();
            }
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return RedirectToAction("ForgotPasswordConfirm");
            }
            //preparar o link para o envio do e-mail 
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeToken = HttpUtility.UrlEncode(token);
            var callbackUrl = Url.Action("ResetPassword", "Accounts", new {userId=user.Id, token = encodeToken}, Request.Scheme);
            // preparar dados do email
            var assunto = "Redefinição de senha";
            var corpo = $"Clique no link para redefinir sua senha: <a href = '{callbackUrl}'>Redefinir senha</a>";
            // Enviar email 
            await _emailService.SendEmailAsync(email, assunto, corpo);
            return RedirectToAction("ForgotPasswordConfirm");
        }
    } // classe
} // namespace 
