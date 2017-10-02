namespace CarWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCarWeb : DbContext
    {
        public ModelCarWeb()
            : base("name=ModelCarWeb")
        {
        }

        public virtual DbSet<Arquivos> Arquivos { get; set; }
        public virtual DbSet<Cadastrador> Cadastrador { get; set; }
        public virtual DbSet<Cadastros> Cadastros { get; set; }
        public virtual DbSet<ImportacaoSeia> ImportacaoSeia { get; set; }
        public virtual DbSet<Lotes> Lotes { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }
        public virtual DbSet<Propietario> Propietario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Acessos> Acessos { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivos>()
                .Property(e => e.cadastrador)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastrador>()
                .Property(e => e.cadastrador1)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.propietario)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.area)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.statusservico)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.observacao)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.cadastrador)
                .IsUnicode(false);

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.statusemissao)
                .IsFixedLength();

            modelBuilder.Entity<Cadastros>()
                .Property(e => e.statusimpressao)
                .IsFixedLength();

            modelBuilder.Entity<ImportacaoSeia>()
                .Property(e => e.nomeimovel)
                .IsUnicode(false);

            modelBuilder.Entity<ImportacaoSeia>()
                .Property(e => e.requerente)
                .IsUnicode(false);

            modelBuilder.Entity<ImportacaoSeia>()
                .Property(e => e.area)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Lotes>()
                .Property(e => e.lote)
                .IsFixedLength();

            modelBuilder.Entity<Municipios>()
                .Property(e => e.uf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Propietario>()
                .Property(e => e.propietario1)
                .IsUnicode(false);

            modelBuilder.Entity<Cidade>()
                .Property(e => e.uf)
                .IsFixedLength();

            modelBuilder.Entity<usuarios>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.senha)
                .IsUnicode(false);
        }
    }
}
