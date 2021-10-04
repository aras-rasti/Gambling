using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);

        System.Threading.Tasks.Task InsertAsync(T entity);
        System.Threading.Tasks.Task InsertRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        System.Threading.Tasks.Task UpdateAsync(T entity);
        System.Threading.Tasks.Task UpdateRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        System.Threading.Tasks.Task DeleteAsync(T entity);
        System.Threading.Tasks.Task DeleteRangeAsync(IEnumerable<T> entities);

        T GetById(System.Object id);

        System.Threading.Tasks.Task<T> GetByIdAsync(System.Object id);

        bool DeleteById(System.Object id);

        System.Threading.Tasks.Task<bool> DeleteByIdAsync(System.Object id);

        /// <summary>
        /// حضور این تابع اصلا حرفه‌ای نیست
        /// </summary>
        /// <returns></returns>
        //System.Linq.IQueryable<T> Get();

        System.Collections.Generic.IList<T> GetAll();

        System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> GetAllAsync();

        System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> FindByConditionAsync
        (Expression<Func<T, bool>> filter = null
            , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
            , params Expression<Func<T, System.Object>>[] includes);
    }
}
