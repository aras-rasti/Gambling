using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.ViewModel.User;

namespace Gambling.Services.Contracts
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(UserViewModel User);
    }
}
