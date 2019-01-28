using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.UsersOnline
{
    public class User
    {
        private List<string> Chats = new List<string>();
        public readonly string ContextId;

        public User(string contextId)
        {
            ContextId = contextId;
        }

        public void AddUserToChat(string chatName)
        {
            if (Chats.Count != 0) DeleteUserFromChats();
            Chats.Add(chatName);
        }

        private void DeleteUserFromChats()
        {
            Chats.Clear();
        }
    }
}
