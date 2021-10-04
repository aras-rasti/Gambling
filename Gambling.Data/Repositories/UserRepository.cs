using Gambling.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gambling.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Gambling.Data.Repositories
{
    public class UserRepository: Repository<User,ApiContext>, IUserRepository
    {
        public UserRepository(ApiContext databaseContext):base(databaseContext)
        {
            
        }
        private async Task<List<User>> GetUsersFromFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MockData/UserStore.json");

            var openStream = await File.ReadAllTextAsync(path);

            return JsonConvert.DeserializeObject<List<User>>(openStream);
        }

        public async Task<User> GetByUserName(string username)
        {

            var users =  DatabaseContext.Users.Local.FirstOrDefault(u => u.UserName == username);
            return users;
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            var users = DatabaseContext.Users.Local.ToList();
            if (users.Count>0)
            {
                var currentUser = users.FirstOrDefault(u => u.Password == password);
                if (currentUser!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
