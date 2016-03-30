using System;
using System.Linq;
using DDDPoC.Domain.Models;
using NUnit.Framework;

namespace DDDPoC.Domain.Test
{
  [TestFixture]
  public class WhenAddingAFeedbackForAShop
  {
    private Shop _shop;

    [SetUp]
    public void BeforeEach()
    {
      _shop = Shop.CreateForMerchant(CombGuid.Generate(), "Sample shop");
    }

    [Test]
    public void The_shop_has_one_new_feedback()
    {
      // arrange
      var count = _shop.Feedbacks.Count();

      // act
      _shop.AddFeedback("Sample text", 3, "user");

      // assert
      Assert.AreEqual(count + 1, _shop.Feedbacks.Count());
    }

    [Test]
    public void The_average_score_is_updated()
    {
      // arrange
      var avg = _shop.AverageScore;
      var count = _shop.Feedbacks.Count();

      // act
      _shop.AddFeedback("Sample text", 3, "user");

      // assert
      Assert.AreEqual(((avg ?? 0 * count) + 3) / (count + 1), _shop.AverageScore);
    }

    [Test]
    [ExpectedException(typeof(Exception), ExpectedMessage = "already", MatchType = MessageMatch.Contains)]
    public void An_exception_is_raised_if_the_user_has_already_submitted_his_feedback()
    {
      // arrange
      _shop.AddFeedback("Sample text", 3, "user");

      // act
      _shop.AddFeedback("Sample text new", 3, "user");
    }
  }
}
