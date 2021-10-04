using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Data.Contracts;
using Gambling.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Data
{
	public class UnitOfWork : object, IUnitOfWork
	{

		public UnitOfWork(Tools.Options options) : base()
		{
			Options = options;
		}

        private IUserRepository _userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository =
                        new UserRepository(DatabaseContext);
                }

                return _userRepository;
            }
        }

		protected Tools.Options Options { get; set; }

		private ApiContext _databaseContext;

		internal ApiContext DatabaseContext
		{
			get
			{
				if (_databaseContext == null)
				{
					var optionsBuilder =
						new DbContextOptionsBuilder<ApiContext>();

					switch (Options.Provider)
					{
						case Tools.Enums.Provider.SqlServer:
							{
								//optionsBuilder.UseSqlServer
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Tools.Enums.Provider.MySql:
							{
								//optionsBuilder.UseMySql
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Tools.Enums.Provider.Oracle:
							{
								//optionsBuilder.UseOracle
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Tools.Enums.Provider.PostgreSQL:
							{
								//optionsBuilder.UsePostgreSQL
								//	(connectionString: Options.ConnectionString);

								break;
							}

						case Tools.Enums.Provider.InMemory:
							{
								optionsBuilder.UseInMemoryDatabase(databaseName: "StakeDb");

								break;
							}

						default:
							{
								break;
							}
					}

					_databaseContext =
						new ApiContext(options: optionsBuilder.Options);
				}

				return _databaseContext;
			}
		}


        public IRepository<T> GetRepository<T>() where T : class
		{
			return new Repository<T, ApiContext>(DatabaseContext);
		}


		public virtual void Save()
		{
			DatabaseContext.SaveChanges();
		}

		public virtual async System.Threading.Tasks.Task SaveAsync()
		{
			await DatabaseContext.SaveChangesAsync();
		}



		// **********
		/// <summary>
		/// To detect redundant calls
		/// </summary>
		public bool IsDisposed { get; protected set; }
		// **********

		/// <summary>
		/// Public implementation of Dispose pattern callable by consumers.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

			System.GC.SuppressFinalize(this);
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
			{
				return;
			}

			if (disposing)
			{
				// TODO: dispose managed state (managed objects).

				if (_databaseContext != null)
				{
					_databaseContext.Dispose();
					_databaseContext = null;
				}
			}

			// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
			// TODO: set large fields to null.

			IsDisposed = true;
		}

		~UnitOfWork()
		{
			Dispose(false);
		}
	}
}
