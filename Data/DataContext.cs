using Erp_Jornada.Model;
using Microsoft.EntityFrameworkCore;

namespace Erp_Jornada.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        DbSet<Usuario> Usuario { get; set; }
        DbSet<Marca> Marca { get; set; }
        DbSet<Marca> Fabrica { get; set; }
    }
}
