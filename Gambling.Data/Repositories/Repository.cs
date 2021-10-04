using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Gambling.Data.Contracts;

namespace Gambling.Data.Repositories
{
	public class Repository<T, TContext> : object, IRepository<T> where T : class where TContext : DbContext

	{
		internal Repository(TContext databaseContext) : base()
		{
			DatabaseContext =
				databaseContext ?? throw new System.ArgumentNullException(paramName: nameof(databaseContext));

			DbSet = DatabaseContext.Set<T>();
		}

		internal TContext DatabaseContext { get; }

		internal Microsoft.EntityFrameworkCore.DbSet<T> DbSet { get; }

		public virtual void Insert(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}

			DbSet.Add(entity);
		}
		public virtual async void InsertRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}

			DbSet.AddRange(entities);
		}

		public virtual async System.Threading.Tasks.Task InsertAsync(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}

			await DbSet.AddAsync(entity);
		}
		public virtual async System.Threading.Tasks.Task InsertRangeAsync(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}

			await DbSet.AddRangeAsync(entities);
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}

			DbSet.Update(entity);
		}
		public virtual void UpdateRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}

			DbSet.UpdateRange(entities);
		}

		public virtual async System.Threading.Tasks.Task UpdateAsync(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}


			await System.Threading.Tasks.Task.Run(() =>
			{
				DbSet.Update(entity);
			});
		}
		public virtual async System.Threading.Tasks.Task UpdateRangeAsync(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}


			await System.Threading.Tasks.Task.Run(() =>
			{
				DbSet.UpdateRange(entities);
			});
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}

			DbSet.Remove(entity);

		}
		public virtual void DeleteRange(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}

			DbSet.RemoveRange(entities);

		}

		public virtual async System.Threading.Tasks.Task DeleteAsync(T entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entity));
			}

			await System.Threading.Tasks.Task.Run(() =>
			{
				DbSet.Remove(entity);
			});
		}
		public virtual async System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<T> entities)
		{
			if (entities == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(entities));
			}

			await System.Threading.Tasks.Task.Run(() =>
			{
				DbSet.RemoveRange(entities);
			});
		}

		public virtual T GetById(System.Object id)
		{
			return DbSet.Find(keyValues: id);
		}

		public virtual async System.Threading.Tasks.Task<T> GetByIdAsync(System.Object id)
		{
			return await DbSet.FindAsync(keyValues: id);
		}

		public virtual bool DeleteById(System.Object id)
		{
			T entity = GetById(id);

			if (entity == null)
			{
				return false;
			}

			Delete(entity);

			return true;
		}

		public virtual async System.Threading.Tasks.Task<bool> DeleteByIdAsync(System.Object id)
		{
			T entity =
				await GetByIdAsync(id);

			if (entity == null)
			{
				return false;
			}

			await DeleteAsync(entity);

			return true;
		}

		public virtual System.Collections.Generic.IList<T> GetAll()
		{

			var result =
				DbSet.ToList();

			return result;
		}

		public virtual async System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> GetAllAsync()
		{

			var result =
				await DbSet.ToListAsync();

			return result;

		}

		public async System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> FindByConditionAsync
		(Expression<Func<T, bool>> filter = null
			, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
			, params Expression<Func<T, System.Object>>[] includes)
		{
			try
			{
				IQueryable<T> query = DbSet;

				foreach (var include in includes)
				{
					query = query.Include(include);
				}

				if (filter != null)
				{
					query = query.Where(filter);
				}

				if (orderBy != null)
				{
					query = orderBy(query);
				}

				var result = await query.ToListAsync();
				return result;
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
