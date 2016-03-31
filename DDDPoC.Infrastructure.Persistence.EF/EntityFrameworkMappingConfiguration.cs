using System.Collections.Generic;
using System.Reflection;
using DelegateDecompiler;

namespace DDDPoC.Infrastructure.Persistence.EF
{
  /// <summary>
  /// Configuration to dynamically replace IEnumerable with ICollection
  /// </summary>
  /// <remarks>
  /// Rewrite expressions that refer to the IEnumerable property to prevent
  /// exposing a child collection as ICollection, yet allowing EF to map and track it
  /// </remarks>
  public class EntityFrameworkMappingConfiguration : DefaultConfiguration
  {
    #region Data

    private readonly HashSet<MemberInfo> _members = new HashSet<MemberInfo>();

    #endregion

    #region Apis
    
    public void RegisterForDecompilation(MemberInfo member)
    {
      _members.Add(member);
    }

    public override bool ShouldDecompile(MemberInfo member)
    {
      return _members.Contains(member) || base.ShouldDecompile(member);
    }

    #endregion
  }
}
