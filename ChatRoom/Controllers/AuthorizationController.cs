using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ChatRoom.DbUserAccounts;
using ChatRoom.Models.AuthorizationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatRoom.Controllers
{
    public class AuthorizationController : Controller
    {
        private UserAccountContext userAccountContext;

        public AuthorizationController(UserAccountContext accountContext)
        {
            ConsoleMassage("Created BD of users in AccountController");
            userAccountContext = accountContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ConsoleMassage("Login HttpGet");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ConsoleMassage("Login HttpPost");
            if (ModelState.IsValid)
            {
                UserAccount userAccount = await userAccountContext.UserAccounts.FirstOrDefaultAsync(u
                    => u.Name == loginModel.Name && u.Password == loginModel.Password);
                if(userAccount != null)
                {
                    await Authenticate(loginModel.Name);
                    return RedirectToAction("OnlineChat", "Chat");
                }
                ModelState.AddModelError("", "Incorrect name or password");
            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ConsoleMassage("Register HttpGet");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            ConsoleMassage("Register HttpPost");
            if (ModelState.IsValid)
            {
                UserAccount userAccount = await userAccountContext.UserAccounts.FirstOrDefaultAsync(u
                    => u.Name == registerModel.Name);
                if(userAccount != null)
                {
                    userAccountContext.Add( new { Name = registerModel.Name, Password = registerModel.Password} );
                    await userAccountContext.SaveChangesAsync();
                    await Authenticate(registerModel.Name);
                    return RedirectToAction("OnlineChat", "Chat");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect name or password");
                }
            }
            return View(registerModel);
        }

        private async Task Authenticate(string userName)
        {
            ConsoleMassage("Authenticate");
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            ConsoleMassage("Logout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }

        private void ConsoleMassage(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
