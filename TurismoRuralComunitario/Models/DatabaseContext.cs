using System.Data.Entity;

namespace TurismoRuralComunitario.Models
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(null);
        }
        private readonly string schema = "usuarios";
        public DatabaseContext()
            : base("name=Conexion")
        {
        }

        #region Dbset Tablas database context
        public DbSet<Usuario> TablaUsuarios { get; set; }
        public DbSet<Rol> TablaRoles { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema(this.schema);
            base.OnModelCreating(builder);
        }
    }
}