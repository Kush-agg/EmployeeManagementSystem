using System.Linq.Expressions;

namespace EMS.Repository.IRepository
{
    public interface IRepository<T>  where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        T GetFirstOrDefault(Expression<Func<T,bool>> filter);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}