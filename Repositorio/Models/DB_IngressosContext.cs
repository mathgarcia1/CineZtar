using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repositorio.Models
{
    public partial class DB_IngressosContext : DbContext
    {
        public DB_IngressosContext()
        {
        }

        public DB_IngressosContext(DbContextOptions<DB_IngressosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<CompraIngresso> CompraIngressos { get; set; }
        public virtual DbSet<Filme> Filmes { get; set; }
        public virtual DbSet<FilmeGenero> FilmeGeneros { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<Ingresso> Ingressos { get; set; }
        public virtual DbSet<StatusCompra> StatusCompras { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=. ; Database=DB_Ingressos; integrated security=true;");
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

                entity.Property(e => e.IdIngresso).HasColumnName("id_ingresso");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.QtdIngressos).HasColumnName("qtd_ingressos");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdIngressoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdIngresso)
                    .HasConstraintName("FK_Carrinho_Ingresso");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Carrinho_Usuario");
            });

            modelBuilder.Entity<CompraIngresso>(entity =>
            {
                entity.HasKey(e => e.IdCompraIngresso);

                entity.ToTable("Compra_Ingresso");

                entity.Property(e => e.IdCompraIngresso)
                    .ValueGeneratedNever()
                    .HasColumnName("id_compra_ingresso");

                entity.Property(e => e.IdCompra).HasColumnName("id_compra");

                entity.Property(e => e.IdIngresso).HasColumnName("id_ingresso");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");
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

                entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");

                entity.Property(e => e.Imagem).HasColumnName("imagem");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.HasOne(d => d.IdIdiomaNavigation)
                    .WithMany(p => p.Filmes)
                    .HasForeignKey(d => d.IdIdioma)
                    .HasConstraintName("FK_Filme_Idioma");
            });

            modelBuilder.Entity<FilmeGenero>(entity =>
            {
                entity.HasKey(e => e.IdFilmeGenero);

                entity.ToTable("Filme_Genero");

                entity.Property(e => e.IdFilmeGenero).HasColumnName("id_filme_genero");

                entity.Property(e => e.IdFilme).HasColumnName("id_filme");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.FilmeGeneros)
                    .HasForeignKey(d => d.IdFilme)
                    .HasConstraintName("FK_Filme_Genero_Filme");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.FilmeGeneros)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK_Filme_Genero_Genero");
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

            modelBuilder.Entity<Ingresso>(entity =>
            {
                entity.HasKey(e => e.IdIngresso);

                entity.ToTable("Ingresso");

                entity.Property(e => e.IdIngresso).HasColumnName("id_ingresso");

                entity.Property(e => e.IdFilme).HasColumnName("id_filme");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.Ingressos)
                    .HasForeignKey(d => d.IdFilme)
                    .HasConstraintName("FK_Ingresso_Filme");
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
