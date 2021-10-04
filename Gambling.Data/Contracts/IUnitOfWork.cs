using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Data.Contracts
{
    public interface IUnitOfWork : System.IDisposable
    {
        bool IsDisposed { get; }

        void Save();

        System.Threading.Tasks.Task SaveAsync();

        IRepository<T> GetRepository<T>() where T : class;

        IUserRepository UserRepository { get; }
    }
}
