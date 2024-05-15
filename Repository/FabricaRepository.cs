using Erp_Jornada.Data;
using Erp_Jornada.Model;
using Microsoft.EntityFrameworkCore;

namespace Erp_Jornada.Repository
{
    public class FabricaRepository(DataContext context) : Repository<Fabrica>(context)
    {
        private readonly DataContext _context = context;
    
        public async Task<bool> AlredyExist(string email)
             => await _context.Set<Fabrica>().AnyAsync(u => u.Ativo && u.Email == email);
    }
}
