using Erp_Jornada.Model;

namespace Erp_Jornada.Repository
{
    public interface IRepository<T> where T : Entidade
    {
        Task<IList<T>> GetItens();
        Task<T> GetById(int? id);
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Remove(T obj);
        Task<IList<T>> GetItemsPaginated(int pageNumber, int pageSize);
        Task<int> CountItens();
    }
}
