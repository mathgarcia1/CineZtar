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

        public virtual DbSet<Carrinho> Carrinhos { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Filme> Filmes { get; set; }
        public virtual DbSet<FilmeGenero> FilmeGeneros { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<Ingresso> Ingressos { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }
        public virtual DbSet<TipoIngresso> TipoIngressos { get; set; }
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

            modelBuilder.Entity<Carrinho>(entity =>
            {
                entity.HasKey(e => e.IdCarrinho);

                entity.ToTable("Carrinho");

                entity.Property(e => e.IdCarrinho).HasColumnName("id_carrinho");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("data");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.QtdIngressos).HasColumnName("qtd_ingressos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Carrinhos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Carrinho_Usuario");
            });

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(e => e.IdCinema);

                entity.ToTable("Cinema");

                entity.Property(e => e.IdCinema).HasColumnName("id_cinema");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.TotalSalaCinema).HasColumnName("total_sala_cinema");
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

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.IdTipoIngresso).HasColumnName("id_tipo_ingresso");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.Ingressos)
                    .HasForeignKey(d => d.IdSala)
                    .HasConstraintName("FK_Ingresso_Sala");

                entity.HasOne(d => d.IdTipoIngressoNavigation)
                    .WithMany(p => p.Ingressos)
                    .HasForeignKey(d => d.IdTipoIngresso)
                    .HasConstraintName("FK_Ingresso_Tipo_Ingresso");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSala);

                entity.ToTable("Sala");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.IdCinema).HasColumnName("id_cinema");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.TotalAssento).HasColumnName("total_assento");

                entity.HasOne(d => d.IdCinemaNavigation)
                    .WithMany(p => p.Salas)
                    .HasForeignKey(d => d.IdCinema)
                    .HasConstraintName("FK_Sala_Cinema");
            });

            modelBuilder.Entity<TipoIngresso>(entity =>
            {
                entity.HasKey(e => e.IdTipoIngresso);

                entity.ToTable("Tipo_Ingresso");

                entity.Property(e => e.IdTipoIngresso).HasColumnName("id_tipo_ingresso");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
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
