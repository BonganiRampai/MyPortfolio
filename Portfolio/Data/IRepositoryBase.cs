using Portfolio.Data.DataAccess;

namespace Portfolio.Data
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetWithOptions(QueryOptions<T> options);
    }

}
