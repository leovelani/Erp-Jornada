using Erp_Jornada.Data;
using Erp_Jornada.Model;
using Microsoft.EntityFrameworkCore;

namespace Erp_Jornada.Repository
{
    public class UsuarioRepository(DataContext context) : Repository<Usuario>(context)
    {
        private readonly DataContext _context = context;

        public async Task<bool> AlredyExist(string email)
            => await _context.Set<Usuario>().AnyAsync(u => u.Ativo && u.Email == email);

        public async Task<Usuario> GetByEmail(string email)
        => await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Ativo && u.Email == email);
    }
}
