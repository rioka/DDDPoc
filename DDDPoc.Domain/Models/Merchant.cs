using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDDPoC.Domain.Models
{
  /// <summary>
  /// Merchant
  /// </summary>
  public class Merchant : IAggregate
  {
    #region Properties
    
    /// <summary>
    /// Id of the instance
    /// </summary>
    #endregion
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// List of owned shops
    /// </summary>
    public IEnumerable<Shop> Shops { get { return _shops; } }

    #region Private & protected data
    
    /// <summary>
    /// Updatable list of shops
    /// </summary>
    protected ICollection<Shop> _shops { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Get a new instance
    /// </summary>
    /// <param name="id">Id of the merchant</param>
    /// <param name="name">Name</param>
    public Merchant(Guid id, string name) : this()
    {
      Id = id;
      Name = name;
    }

    /// <summary>
    /// Get a new instance
    /// </summary>
    /// <remarks>Used by Entity Framework</remarks>
    private Merchant()
    {
      _shops = new Collection<Shop>();
    }

    #endregion

    #region Behavior

    /// <summary>
    /// Remove a shop
    /// </summary>
    /// <param name="shop">Shop to remove</param>
    public void RemoveShop(Shop shop)
    {
      _shops.Remove(shop);
    }

    /// <summary>
    /// Add a new shop
    /// </summary>
    /// <param name="name">Name of the shop</param>
    /// <returns>The new shop</returns>
    public Shop AddShop(string name)
    {
      var shop = Shop.CreateForMerchant(Id, name);
      _shops.Add(shop);
      return shop;
    }

    #endregion
  }
}
