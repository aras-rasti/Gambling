using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Entities.User;

namespace Gambling.Services.Contracts
{
    public interface IApplicationUserManager
    {
        Task<User> FindByNameAsync(string userName);
    }
}
