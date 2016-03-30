using System.Linq;
using DDDPoC.Domain;
using DDDPoC.Domain.Models;
using NUnit.Framework;

namespace DDDPoc.Domain.Test
{
  [TestFixture]
  public class WhenAddingAShopToAMerchant
  {
    [Test]
    public void The_merchant_has_a_new_shop()
    {
      // arrange
      var merchant = new Merchant(CombGuid.Generate(), "A merchant");
      var count = merchant.Shops.Count();

      // act
      merchant.AddShop("Sample shop");

      // assert
      Assert.AreEqual(count + 1, merchant.Shops.Count());
    }
  }
}
