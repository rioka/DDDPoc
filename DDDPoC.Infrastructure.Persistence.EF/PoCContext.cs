using System.Data.Entity;
using DDDPoC.Domain.Models;

namespace DDDPoC.Infrastructure.Persistence.EF
{
  public class PoCContext : DbContext
  {
    public DbSet<Merchant> Merchants { get; set; }

    static PoCContext()
    {
      //Database.SetInitializer<PoCContext>(null);
    }

    public PoCContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {}

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      // Run mappings
      modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
      
      base.OnModelCreating(modelBuilder);
    }
  }
}
