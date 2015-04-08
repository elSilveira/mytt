using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace mytt.Models
{
    public class Contexto : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<mytt.Models.Contexto>());

        public Contexto()
            : base("name=Contexto")
        {
        }
        
        public DbSet<DM> DM { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Seguidores> Seguidores { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<Pesquisa>();

            #region DM
            modelBuilder.Entity<DM>()
                    .HasRequired(h => h.UsuarioRemetente)
                    .WithMany()
                    .HasForeignKey(h => h.IdUsuarioRemetente)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<DM>()
                .HasRequired(h => h.UsuarioDestino)
                .WithMany()
                .HasForeignKey(h => h.IdUsuarioDestino)
                .WillCascadeOnDelete(false); 
            #endregion

            #region Post
            modelBuilder.Entity<Post>()
                    .HasRequired(h => h.Usuario)
                    .WithMany()
                    .HasForeignKey(h => h.IdUsuario)
                    .WillCascadeOnDelete(false);
            #endregion

            #region Seguidores
            modelBuilder.Entity<Seguidores>().HasKey(h => new {h.IdUsuarioSeguidor, h.IdUsuarioSeguido });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
