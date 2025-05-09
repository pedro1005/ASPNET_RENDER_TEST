using Microsoft.EntityFrameworkCore;
using EisntMvc.Models;

namespace EisntMvc.Data;


public class EisntDbContext : DbContext
{
    public EisntDbContext(DbContextOptions<EisntDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProdutosModel> Produtos { get; set; }
    public DbSet<CategoriaModel> CategoriaModel { get; set; }
    public DbSet<FornecedorModel> FornecedorModel { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<ProdutosModel>()
            .HasOne<CategoriaModel>()
            .WithMany()
            .HasForeignKey(p => p.Categoria_Id)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        modelBuilder.Entity<ProdutosModel>()
            .HasOne<FornecedorModel>()
            .WithMany()
            .HasForeignKey(p => p.Fornecedor_Id)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
    }
    
}