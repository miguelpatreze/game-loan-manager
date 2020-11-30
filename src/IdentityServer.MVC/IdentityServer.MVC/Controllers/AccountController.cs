using IdentityServer.MVC.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IdentityServer.MVC.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _interaction = interaction;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context?.Client.ClientId != null)
                return View(new LoginViewModel(returnUrl));

            return NotFound();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (context != null)
                        return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError(string.Empty, "Usuário ou Senha Inválidos");
            }

            return View(model);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (string.IsNullOrEmpty(logoutId))
                return NotFound();

            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrWhiteSpace(logoutRequest?.PostLogoutRedirectUri) == true )
                return NotFound();

            if (HttpContext.User?.Identity?.IsAuthenticated == true)
                await _signInManager.SignOutAsync();

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
