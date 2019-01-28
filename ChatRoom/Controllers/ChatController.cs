using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ChatRoom.Models.AuthorizationModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using ChatRoom.Chats;
using ChatRoom.Models;


namespace ChatRoom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public ChatRooms Chats { get; set; }

        public ChatController()
        {
            Chats = new ChatRooms();
        }

        [HttpGet]
        public IActionResult ChatList()
        {
            return View(Chats.GetListOfChats);
        }

        [HttpGet]
        public IActionResult AddChatRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AddChatRoom(ChatModel chatModel)
        {
            await Chats.Add(chatModel.Name);
            return RedirectToAction("ChatList", "Chat");
        }

        [HttpGet]
        public IActionResult DeleteChatRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChatRoom(ChatModel chatModel)
        {
            await Chats.Delete(chatModel.Name);
            return RedirectToAction("ChatList", "Chat");
        }

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