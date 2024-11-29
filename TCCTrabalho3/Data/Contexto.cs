using Microsoft.EntityFrameworkCore;
using TCCTrabalho3.Models;

namespace TCCTrabalho3.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<VendasModel> Vendas { get; set; }
        public DbSet<CursosModel> Cursos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoItem> CarrinhosItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarrinhoItem>().ToTable("CarrinhoItem");
            modelBuilder.Entity<Carrinho>().ToTable("Carrinho");
            modelBuilder.Entity<Pedidos>().ToTable("Pedidos");
            modelBuilder.Entity<CursosModel>().ToTable("CursosModel");
            modelBuilder.Entity<VendasModel>().ToTable("VendasModel");
        }
    }
}
