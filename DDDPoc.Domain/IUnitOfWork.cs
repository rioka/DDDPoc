
namespace DDDPoC.Domain
{
  /// <summary>
  /// Unit of work
  /// </summary>
  public interface IUnitOfWork
  {
    /// <summary>
    /// Persist all changes
    /// </summary>
    void Commit();
    
    /// <summary>
    /// Drop pending changes
    /// </summary>
    void Clear();
  }
}
