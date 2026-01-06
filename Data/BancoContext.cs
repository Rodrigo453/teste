using AlibiPerfeito_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AlibiPerfeito_CRUD.Data;

public class BancoContext : DbContext
{
    public DbSet<DesculpaModel> Desculpas { get; set; }
    public DbSet<Categoria> Categoria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        //Banco Antigo
        //optionsBuilder.UseNpgsql("Host=dpg-cthid523esus73b6tu80-a.oregon-postgres.render.com;Port=5432;Username=alibiperfeitodb_user;Password=0YRdaXaLCk5De7LyDrACCfihB2Tsmqv3;Database=alibiperfeitodb;");
        //Banco novo
        optionsBuilder.UseNpgsql("Host=dpg-cupkpltds78s7393q9m0-a.oregon-postgres.render.com;Port=5432;Username=alibiperfeitodb_user;Password=emKdPSEBPZd5QMLg6DNQguD4LujdXvJm;Database=alibiperfeitodb_kr9z;");
        //optionsBuilder.UseNpgsql("postgresql://alibiperfeitodb_user:0YRdaXaLCk5De7LyDrACCfihB2Tsmqv3@dpg-cthid523esus73b6tu80-a.oregon-postgres.render.com/alibiperfeitodb");
        //optionsBuilder.UseSqlite("Data Source=desculpas.db"); // Substitua conforme seu ambiente
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar o schema para cada tabela
        modelBuilder.Entity<DesculpaModel>()
            .ToTable("Desculpas", "AlibiPerfeito");  // Especifique o schema que deseja (ex: "meuSchema")

        modelBuilder.Entity<Categoria>()
            .ToTable("Categorias", "AlibiPerfeito");  // Especifique o schema para a Categoria
    }
}