using Microsoft.EntityFrameworkCore;
using ClienteAPI.Models;

namespace ClienteAPI.Data
{
    public class BdClientesContext : DbContext
    {
        public BdClientesContext(DbContextOptions<BdClientesContext> options)
            : base(options)
        {
        }

        public DbSet<TipoDocumento> TiposDocumentos { get; set; }
    }
}
