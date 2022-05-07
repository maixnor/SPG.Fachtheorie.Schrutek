using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Exceptions;
using Spg.ProbeFachtheorie.Aufgabe2.Domain.Interfaces;
using Spg.ProbeFachtheorie.Aufgabe3.Dtos;
using Spg.ProbeFachtheorie.Aufgabe3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.ProbeFachtheorie.Aufgabe3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet()]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromForm] ApplicationUserDto model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authService.Login(model.Email, model.Password);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    return LocalRedirect("/Home/Index");
                }
                catch (AuthServiceException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                return View();
            }
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return LocalRedirect("/Account/SignedOut");
        }

        [HttpGet()]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }
            return View();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View("AccessDenied", returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
