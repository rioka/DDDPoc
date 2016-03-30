using System.Data.Entity.ModelConfiguration;
using DDDPoC.Domain.Models;

namespace DDDPoC.Infrastructure.Persistence.EF.Mappings
{
  /// <summary>
  /// Mapping for <see cref="Merchant"/>
  /// </summary>
  internal class MerchantMapping : EntityTypeConfiguration<Merchant>
  {
    #region Constructor

    public MerchantMapping()
    {
      HasKey(m => m.Id);

      #region Properties

      Property(m => m.Name)
        .IsRequired()
        .HasMaxLength(100);

      #endregion

      #region Relations

      this.HasMany(m => m.Shops);

      #endregion
    }

    #endregion
  }
}
