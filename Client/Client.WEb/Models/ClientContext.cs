using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Client.WEb.Models
{
    public partial class ClientContext : DbContext
    {
        public ClientContext()
            : base("name=ClientContext")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.NameGenre)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Client)
                .WithOptional(e => e.Genre1)
                .HasForeignKey(e => e.Genre);
        }
    }
}
