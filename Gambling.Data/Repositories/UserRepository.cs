using Gambling.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Gambling.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        public async Task<User> GetByUserAndPass(string username)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dirPath = Assembly.GetExecutingAssembly().Location;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"UserStore.json");


            string fileName = path;
            using FileStream openStream = File.OpenRead(fileName);
            var users =
                await JsonSerializer.DeserializeAsync<User>(openStream);

            return new User();



        }
    }
}
