using System.Data.Entity.ModelConfiguration;
using DDDPoC.Domain.Models;

namespace DDDPoC.Infrastructure.Persistence.EF.Mappings
{
  /// <summary>
  /// Mapping for <see cref="Shop"/>
  /// </summary>
  internal class ShopMapping : EntityTypeConfiguration<Shop>
  {
    #region Constructor

    public ShopMapping()
    {
      HasKey(m => m.Id);

      #region Properties

      Property(m => m.Name)
        .IsRequired()
        .HasMaxLength(100);
      Ignore(m => m.AverageScore);

      Property(m => m.Version)
        .IsRowVersion();

      #endregion

      #region Relations

      this.HasMany(m => m.Feedbacks);

      #endregion
    }

    #endregion
  }
}
