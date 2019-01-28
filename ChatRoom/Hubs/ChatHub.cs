using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatRoom.UsersOnline;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("*****************************************************");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("*****************************************************");
            var temp = new ControlUsers();
            temp.DeleteDisconnectedUser(Context.ConnectionId);
        }

        public async Task AddUserToChat(string chatName)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, chatName);
        }

        public async Task DeleteUserFromChat(string chatName)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        }

        public async Task SendMessage(string message)
        {
            var user = Context.User;
            await Clients.All.SendAsync("ReceiveMessage", user.Identity.Name, message);
        }
    }
}
