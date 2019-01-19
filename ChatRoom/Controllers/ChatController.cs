using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ChatRoom.Models.AuthorizationModel;
using Microsoft.AspNetCore.Authentication;
using ChatRoom.ConsoleReport;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ChatRoom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult OnlineChat()
        {
            return View();
        }

        public async Task <IActionResult> Logout()
        {
            Info.Output("Logout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }
    }
}