using System.Collections.Generic;
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

        public AuthorizationController(UserAccountContext userAccountContext)
        {
            this.userAccountContext = userAccountContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserAccount userAccount = await userAccountContext.UserAccounts.FirstOrDefaultAsync(u
                    => u.Name == loginModel.Name && u.Password == loginModel.Password);
                if(userAccount != null)
                {
                    await Authenticate(loginModel.Name);
                    return RedirectToAction("ChatList", "Chat");
                }
                ModelState.AddModelError("", "Incorrect name or password");
            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                UserAccount userAccount = await userAccountContext.UserAccounts.FirstOrDefaultAsync(u
                    => u.Name == registerModel.Name);
                if(userAccount == null)
                {
                    userAccountContext.UserAccounts.Add( new UserAccount { Name = registerModel.Name, Password = registerModel.Password } );
                    await userAccountContext.SaveChangesAsync();
                    await Authenticate(registerModel.Name);
                    return RedirectToAction("ChatList", "Chat");
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
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
