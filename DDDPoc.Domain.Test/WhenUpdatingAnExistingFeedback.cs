using System;
using System.Collections.Generic;
using System.Linq;
using DDDPoC.Domain.Models;
using NUnit.Framework;

namespace DDDPoC.Domain.Test
{
  [TestFixture]
  public class WhenUpdatingAnExistingFeedback
  {
    private Shop _shop;
    IList<Guid> _fbIds = new List<Guid>();
      
    [SetUp]
    public void BeforeEach()
    {
      _shop = Shop.CreateForMerchant(CombGuid.Generate(), "Sample shop");
      _fbIds = new List<Guid>();;
      _fbIds.Add(_shop.AddFeedback("First feedback", 3, "userId 1"));
      _fbIds.Add(_shop.AddFeedback("Second feedback", 4, "userId 2"));
    }

    [Test]
    public void The_average_score_is_updated()
    {
      // arrange
      var avg = _shop.AverageScore;

      // act
      _shop.UpdateFeedback(_fbIds.Last(), 3);

      // assert
      Assert.AreEqual(3, _shop.AverageScore);
      Assert.AreEqual(_shop.Feedbacks.Last().Score, 3);
    }
  }
}
