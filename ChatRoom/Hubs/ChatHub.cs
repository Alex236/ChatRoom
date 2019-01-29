using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, string> usersInChats = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            lock (usersInChats)
            {
                usersInChats.Add(Context.ConnectionId, null);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            lock (usersInChats)
            {
                usersInChats.Remove(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        private async Task AddUserToChat(string chatName)
        {
            lock (usersInChats)
            {
                usersInChats[Context.ConnectionId] = chatName;
            }
            Groups.AddToGroupAsync(Context.ConnectionId, chatName);
        }

        private async Task DeleteUserFromChat()
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, usersInChats[Context.ConnectionId]);
            lock (usersInChats)
            {
                usersInChats[Context.ConnectionId] = null;
            }
        }

        public async Task SendMessage(string message)
        {
            var user = Context.User;
            string chat = usersInChats[Context.ConnectionId];
            if(usersInChats[Context.ConnectionId] != null)
                await Clients.Group(chat).SendAsync("ReceiveMessage", user.Identity.Name, message);
        }

        public async Task ChooseChat(string chatName)
        { 
            DeleteUserFromChat();
            AddUserToChat(chatName);
        }
    }
}
