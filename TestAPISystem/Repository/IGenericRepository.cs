namespace TestAPISystem.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task<bool> Delete(int id);
    }
}
