using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Data.Contracts;
using Gambling.Entities.User;
using Gambling.Services.Contracts;
using Gambling.ViewModel.User;

namespace Gambling.Services.Services
{
    public class ApplicationUserManager:IApplicationUserManager
    {
        private readonly IUserRepository _user;

        public ApplicationUserManager(IUserRepository user)
        {
            _user = user;
        }
        public async Task<User> FindByUserNameAsync(string userName)
        {
            //You should use database to restore user ,but because of project scale I avoid this work.

            return await _user.GetByUserName(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            if (user==null)
            {
                return false;
            }
            else
            {
                return await _user.VerifyPasswordAsync(user, password);
            }
        }
    }
}
