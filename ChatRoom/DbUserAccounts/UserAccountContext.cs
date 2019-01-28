using System;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.DbUserAccounts
{
    public class UserAccountContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }

        public UserAccountContext(DbContextOptions<UserAccountContext> option) : base(option)
        {
            Console.WriteLine("UserAccountContext ctor");
            Database.EnsureCreated();
        }
    }
}
