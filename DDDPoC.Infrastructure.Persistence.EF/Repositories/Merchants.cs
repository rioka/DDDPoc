using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DDDPoC.Domain.Models;
using DDDPoc.Domain.Repositories;
using DelegateDecompiler;

namespace DDDPoC.Infrastructure.Persistence.EF.Repositories
{
  /// <summary>
  /// EntityFramework based implementation for <see cref="IMerchants"/>
  /// </summary>
  public class Merchants : IMerchants
  {
    readonly PoCContext _context;

    #region Constructor

    public Merchants(PoCContext context)
    {
      _context = context;
    }

    #endregion

    #region Apis
    
    /// <summary>
    /// Get a merchant
    /// </summary>
    /// <param name="id">Id of the merchant</param>
    /// <returns>The matching merchant or null</returns>
    public Merchant Get(Guid id)
    {
      return _context.Merchants
        .Include(m => m.Shops)
        .FirstOrDefault(m => m.Id == id);
    }

    /// <summary>
    /// Add a new merchant
    /// </summary>
    /// <param name="merchant">Merchant to add</param>
    public void Add(Merchant merchant)
    {
      _context.Merchants.Add(merchant);
    }

    /// <summary>
    /// Find the owner of shops with feedbacks
    /// </summary>
    /// <returns>A list of shops for which at least one feedback is present</returns>
    public IEnumerable<Merchant> FindShopsWithFeedback()
    {
      // TODO wihich one is better?
      // This way it works
      return _context.Merchants
        .Include(m => m.Shops)
        .Include(m => m.Shops.Select(s => s.Feedbacks))
        .Decompile()
        .Where(m => m.Shops.Any(s => s.Feedbacks.Any()))
        .ToArray();
      // Trying this... it works too
      // return _context.Merchants.Include(m => m.Shops).Where(m => m.Shops.Any()).Decompile();
      
      // Trying this... does not work (children not loaded)
      //return _context.Merchants.Where(m => m.Shops.Any()).Decompile().Include(m => m.Shops);
      // Trying this... error executing query (Shops is not supported in linq...), for .Any() I suppose
      //return _context.Merchants.Where(m => m.Shops.Any()).Include(m => m.Shops).Decompile();
      // This way does not work (children not loaded)
      // return _context.Merchants.Decompile().Include(m => m.Shops).Where(m => m.Shops.Any());
    }

    /// <summary>
    /// Get the owner of a shop
    /// </summary>
    /// <param name="shopId">Id of the shop</param>
    /// <returns>The merchant which owns the shop</returns>
    public Merchant FindOwnerOfShop(Guid shopId)
    {
      return _context.Merchants
        .Include(m => m.Shops)
        .Decompile()
        .FirstOrDefault(m => m.Shops.Any(s => s.Id == shopId));
    }

    #endregion
  }
}
