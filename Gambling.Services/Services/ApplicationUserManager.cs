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
        public async Task<User> FindByNameAsync(string userName)
        {
            //You should use database to restore user ,but because of project scale I avoid this work.

            return await _user.GetByUserAndPass("aras");
        }
    }
}
