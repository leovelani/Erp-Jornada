using Erp_Jornada.Data;
using Erp_Jornada.Model;
using Microsoft.EntityFrameworkCore;

namespace Erp_Jornada.Repository
{
    public class MarcaRepository(DataContext context) : Repository<Marca>(context)
    {
        private readonly DataContext _context = context;
    
        public async Task<bool> AlredyExist(string email)
             => await _context.Set<Marca>().AnyAsync(u => u.Ativo && u.Email == email);
    }
}
