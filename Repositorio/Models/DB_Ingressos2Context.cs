using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repositorio.Models
{
    public partial class DB_Ingressos2Context : DbContext
    {
        public DB_Ingressos2Context()
        {
        }

        public DB_Ingressos2Context(DbContextOptions<DB_Ingressos2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<CompraFilme> CompraFilmes { get; set; }
        public virtual DbSet<Filme> Filmes { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<StatusCompra> StatusCompras { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=. ; Database=DB_Ingressos2; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK_Carrinho");

                entity.ToTable("Compra");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("data");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.Property(e => e.IdPreferencia).HasColumnName("id_preferencia");
                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<CompraFilme>(entity =>
            {
                entity.HasKey(e => e.IdCompraFilme).HasName("PK_Compra_Filme");

                entity.ToTable("Compra_Filme");

                entity.Property(e => e.IdCompraFilme).HasColumnName("id_compra_filme");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.IdFilme).HasColumnName("id_filme");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.CompraFilmes)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK_CompraFilme_Compra");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.CompraFilmes)
                    .HasForeignKey(d => d.IdFilme)
                    .HasConstraintName("FK_CompraFilme_Filme");
            });

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.IdFilme);

                entity.ToTable("Filme");

                entity.Property(e => e.IdFilme).HasColumnName("id_filme");

                entity.Property(e => e.AnoLancamento)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ano_lancamento");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.Duracao).HasColumnName("duracao");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");

                entity.Property(e => e.Imagem).HasColumnName("imagem");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.Filmes)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK_Filme_Genero");

                entity.HasOne(d => d.IdIdiomaNavigation)
                    .WithMany(p => p.Filmes)
                    .HasForeignKey(d => d.IdIdioma)
                    .HasConstraintName("FK_Filme_Idioma");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.ToTable("Genero");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.IdIdioma);

                entity.ToTable("Idioma");

                entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<StatusCompra>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("Status_Compra");

                entity.Property(e => e.IdStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("id_status");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(150)
                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipousuario);

                entity.ToTable("Tipo_Usuario");

                entity.Property(e => e.IdTipousuario).HasColumnName("id_tipousuario");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdTipousuario).HasColumnName("id_tipousuario");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.HasOne(d => d.IdTipousuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipousuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Tipo_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
