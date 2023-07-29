using Hoshmand.Core.Entities;
using System.Linq.Expressions;

namespace Hoshmand.Core.Interfaces.Repositories
{
    public interface IGeneralRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
    }
}

