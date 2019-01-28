using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Chats
{
    public class Chat
    {
        public readonly string Name;

        public Chat(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return this.Name == ((Chat)obj).Name;
        }

        public static bool operator == (Chat chat1, Chat chat2)
        {
            return chat1.Equals(chat2);
        }

        public static bool operator !=(Chat chat1, Chat chat2)
        {
            return !chat1.Equals(chat2);
        }
    }
}
