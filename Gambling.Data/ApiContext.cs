using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            LoadUsers();
        }

        public DbSet<User> Users { get; set; }

        public void LoadUsers()
        {
            var testUser1 = new User()
            {
                Id = "1",
                UserName = "aras",
                Password = "123!@#qwe",
                FirstName = "Aras",
                LastName = "Rasti",
                IsActive = true,
                PhoneNumber = "00989128438795"
            };

            Users.Add(testUser1);
        }
    }
}
