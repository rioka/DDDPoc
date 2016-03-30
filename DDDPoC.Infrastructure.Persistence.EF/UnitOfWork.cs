using System;
using DDDPoC.Domain;

namespace DDDPoC.Infrastructure.Persistence.EF
{
  /// <summary>
  /// entity framework implementation for <see cref="IUnitOfWork"/>
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    #region Data
    
    readonly PoCContext _context;

    #endregion

    #region Constructor

    public UnitOfWork(PoCContext context)
    {
      _context = context;
    }

    #endregion

    #region Apis
    
    public void Commit()
    {
      _context.SaveChanges();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
