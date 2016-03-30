using System.Collections.Generic;
using System.Reflection;
using DelegateDecompiler;

namespace DDDPoC.Infrastructure.Persistence.EF
{
  /// <summary>
  /// Configuration to dynamically replace IEnumerable with ICollection
  /// </summary>
  public class EntityFrameworkMappingConfiguration : DefaultConfiguration
  {
    private readonly HashSet<MemberInfo> _members = new HashSet<MemberInfo>();

    public void RegisterForDecompilation(MemberInfo member)
    {
      _members.Add(member);
    }

    public override bool ShouldDecompile(MemberInfo member)
    {
      return _members.Contains(member) || base.ShouldDecompile(member);
    }
  }
}
