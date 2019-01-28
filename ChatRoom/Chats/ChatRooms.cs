using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Chats
{
    public class ChatRooms
    {
        private static List<Chat> chats = new List<Chat>();

        public List<Chat> GetListOfChats
        {
            get { return chats; }
        }

        public async Task Add(string name)
        {
            var newChat = new Chat(name);
            if (chats != null)
            {
                foreach (var chat in chats)
                {
                    if (chat == newChat) return;
                }
                lock (chats)
                {
                    chats.Add(newChat);
                }
            }
        }

        public async Task Delete(string name)
        {
            var newChat = new Chat(name);
            if(chats != null)
            {
                lock (chats)
                {
                    chats.Remove(newChat);
                }
            }
        }
    }
}
