using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDPoC.Domain.Models;

namespace DDDPoc.Domain.Repositories
{
  public interface IMerchants
  {
    /// <summary>
    /// Get a merchant
    /// </summary>
    /// <param name="id">Id of the merchant</param>
    /// <returns>The matching merchant or null</returns>
    Merchant Get(Guid id);

    /// <summary>
    /// Add a new merchant
    /// </summary>
    /// <param name="merchant">Merchant to add</param>
    void Add(Merchant merchant);

    /// <summary>
    /// Find the owner of shops with feedbacks
    /// </summary>
    /// <returns>A list of shops for which at least one feedback is present</returns>
    IEnumerable<Merchant> FindShopsWithFeedback();
    
    /// <summary>
    /// Get the owner of a shop
    /// </summary>
    /// <param name="shopId">Id of the shop</param>
    /// <returns>The merchant which owns the shop</returns>
    Merchant FindOwnerOfShop(Guid shopId);
  }
}
