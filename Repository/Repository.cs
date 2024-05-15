using Erp_Jornada.Data;
using Erp_Jornada.Model;
using Microsoft.EntityFrameworkCore;

namespace Erp_Jornada.Repository
{
    public class Repository<T> : IRepository<T> where T : Entidade
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }


        public async Task<IList<T>> GetItens()
        {
            return await _context.Set<T>().Where(x => x.Ativo).ToListAsync();
        }

        public async Task<T> GetById(int? id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.Ativo);
        }

        public async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Update(T obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Remove(T obj)
        {
            obj.Excluido = true;
            obj.Ativo = false;
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IList<T>> GetItemsPaginated(int pageNumber, int pageSize)
         => await _context.Set<T>()
         .Where(u => u.Ativo)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();

        public async Task<int> CountItens()
            => await _context.Set<T>()
            .CountAsync(u => u.Ativo);
    }
}
