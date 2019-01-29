using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ChatRoom.Models.AuthorizationModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using ChatRoom.Models;


namespace ChatRoom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    { 
        public IActionResult OnlineChatRoom()
        {
            return View();
        }

        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }
    }
}