using System;

namespace DDDPoC.Domain.Models
{
  public class Feedback
  {
    #region Properties
    
    /// <summary>
    /// Id of the instance
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Comment for the feedback
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Score assigned to the shop
    /// </summary>
    public short Score { get; internal set; }

    /// <summary>
    /// Id of the shop
    /// </summary>
    public Guid ShopId { get; private set; }

    /// <summary>
    /// Id of the user submitting the feedback
    /// </summary>
    public string UserId { get; private set; }

    #endregion

    #region Helpers
    
    /// <summary>
    /// Create a new instance
    /// </summary>
    /// <param name="shopId">Id of the shop the feedback refers to</param>
    /// <param name="comment">Comment</param>
    /// <param name="score">assigned score</param>
    /// <param name="userId">Id of the user submitting the feedback</param>
    /// <returns></returns>
    public static Feedback CreateForShop(Guid shopId, string comment, short score, string userId)
    {
      // TODO validate
      return new Feedback() {
        Id = CombGuid.Generate(),
        ShopId = shopId,
        Comment = comment,
        Score = score,
        UserId = userId
      };
    }

    #endregion
  }
}
