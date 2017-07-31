using CapaEntidades;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RelojChecador.Models
{
    public partial class dbContexUsuario : DbContext
    {
        public dbContexUsuario()
            : base("name=SqlConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<EntUsuario> usuario { get; set; }

    }
}