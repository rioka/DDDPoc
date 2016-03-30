using System.Data.Entity.ModelConfiguration;
using DDDPoC.Domain.Models;

namespace DDDPoC.Infrastructure.Persistence.EF.Mappings
{
  /// <summary>
  /// Mapping for <see cref="Feedback"/>
  /// </summary>
  internal class FeedbackMapping : EntityTypeConfiguration<Feedback>
  {
    #region Constructor

    public FeedbackMapping()
    {
      HasKey(m => m.Id);

      #region Properties

      Property(m => m.Comment)
        .IsRequired()
        .HasMaxLength(500);

      #endregion
    }

    #endregion
  }
}
