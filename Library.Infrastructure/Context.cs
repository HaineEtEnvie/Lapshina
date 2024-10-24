using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Library.Infrastructure
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<BookEntity> Book { get; set; }
        public virtual DbSet<ExtraditionEntity> Extradition { get; set; }
        public virtual DbSet<ReadersEntity> Readers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .HasMany(e => e.Extradition)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.idbook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReadersEntity>()
                .HasMany(e => e.Extradition)
                .WithRequired(e => e.Readers)
                .HasForeignKey(e => e.idreaders)
                .WillCascadeOnDelete(false);
        }
    }
}
