using Hoshmand.Core.Entities;
using Hoshmand.Core.Interfaces.Repositories;
using Hoshmand.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hoshmand.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> _dbSet;
        public GeneralRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
            _dbSet = context.Set<T>();

        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
