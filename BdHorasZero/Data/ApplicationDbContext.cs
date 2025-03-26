using BdHorasZero.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BdHorasZero.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.Entity<VinculosModel>()
    //        .Property(v => v.DataInicio)
    //        .HasConversion(
    //            v => v.ToUniversalTime(),
    //            v => v.AddHours(-3));

    //    modelBuilder.Entity<VinculosModel>()
    //        .Property(v => v.DataFim)
    //        .HasConversion(
    //            v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
    //            v => v.HasValue ? v.Value.AddHours(-3) : (DateTime?)null);
    //}

    // adicionados manualmente:
    public DbSet<GestoresModel> TB_Gestores { get; set; }
    public DbSet<FuncionariosModel> TB_Funcionarios { get; set; }
    public DbSet<VinculosModel> TB_Vinculos { get; set; }
    public DbSet<OcorrenciasModel> TB_Ocorrencias { get; set; }


}
