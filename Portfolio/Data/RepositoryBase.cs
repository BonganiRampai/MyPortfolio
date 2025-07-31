using Microsoft.EntityFrameworkCore;
using Portfolio.Data.DataAccess;

namespace Portfolio.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetWithOptions(QueryOptions<T> options)
        {

            IQueryable<T> query = _appDbContext.Set<T>();

            if (options.HasWhere)
                query = query.Where(options.Where);

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            if (options.HasPaging)
            {
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);
            }

            if (options.HasInclude)
            {
                query = query.Include(options.Includes);
            }

            return query.ToList();
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }
    }

}
