using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Data;
using Gambling.Data.Contracts;
using Gambling.Entities.User;
using Gambling.Services.Contracts;
using Gambling.ViewModel.User;

namespace Gambling.Services.Services
{
    public class ApplicationUserManager:IApplicationUserManager
    {
        private readonly ApiContext _ctx;
        private readonly IUnitOfWork _unitOfWork;


        public ApplicationUserManager(IUnitOfWork unitOfWork,ApiContext ctx)
        {
            _unitOfWork = unitOfWork;
            _ctx = ctx;
        }
        public async Task<User> FindByUserNameAsync(string userName)
        {
            //You should use database to restore user ,but because of project scale I avoid this work.
            var us = _ctx.Users.Local.FirstOrDefault(u => u.UserName == userName);
            return await _unitOfWork.UserRepository.GetByUserName(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            if (user==null)
            {
                return false;
            }
            else
            {
                return await _unitOfWork.UserRepository.VerifyPasswordAsync(user, password);
            }
        }
    }
}
