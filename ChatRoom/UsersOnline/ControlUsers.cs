using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.UsersOnline
{
    public class ControlUsers
    {
        private static List<User> UsersConnected = new List<User>();

        public void AddNewConnectedUser(string contextId)
        {
            lock(UsersConnected)
            {
                UsersConnected.Add(new User(contextId));
            }
        }

        public void DeleteDisconnectedUser(string contextId)
        {
            lock (UsersConnected)
            {
                UsersConnected.Remove(new User(contextId));
            }
        }
    }
}
