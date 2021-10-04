using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Entities.User;

namespace Gambling.Data.Contracts
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetByUserName(string username);
        Task<bool> VerifyPasswordAsync(User user, string password);
    }
}
