using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ChatRoom.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        public IActionResult OnlineChat()
        {
            return View();
        }
    }
}