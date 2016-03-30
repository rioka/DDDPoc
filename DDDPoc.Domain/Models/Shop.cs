
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDDPoC.Domain.Models
{
  /// <summary>
  /// Shop
  /// </summary>
  public class Shop : IAggregate
  {
    #region Properties
    
    /// <summary>
    /// Id of the instance
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Name of the shop
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Average score
    /// </summary>
    public float? AverageScore { get; private set; }
    
    /// <summary>
    /// Id of the owner
    /// </summary>
    public Guid MerchantId { get; private set; }

    /// <summary>
    /// submitted feedbacks
    /// </summary>
    public IEnumerable<Feedback> Feedbacks { get { return _feedbacks; } }

    /// <summary>
    /// Concurrency
    /// </summary>
    public byte[] Version { get; set; }

    #endregion

    #region Private & protected data

    /// <summary>
    /// Total score for the shop
    /// </summary>
    private int _totalScore;

    /// <summary>
    /// Updatable list of feedbacks
    /// </summary>
    /// <remarks>To overcome EF limitations and prevent consumers to update the feedback
    /// from outside</remarks>
    protected ICollection<Feedback> _feedbacks { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Create a new instance
    /// </summary>
    private Shop()
    {
      // TODO keep _totalScore consistent when loading from EF
      _feedbacks = new Collection<Feedback>();
      _totalScore = 0;
    }

    #endregion

    #region Factory method

    /// <summary>
    /// Factory method to build consistent instances
    /// </summary>
    /// <param name="merchantId">Id of the owner</param>
    /// <param name="name">Name of the shop</param>
    /// <returns></returns>
    public static Shop CreateForMerchant(Guid merchantId, string name)
    {
      return new Shop() {
        Id = CombGuid.Generate(),
        Name = name,
        MerchantId = merchantId
      };
    }

    #endregion

    #region Behavior

    /// <summary>
    /// Add a new feedback
    /// </summary>
    /// <param name="comment">Comment</param>
    /// <param name="score">Score</param>
    /// <param name="userId">Id of the user submitting the feedback</param>
    /// <returns>Id of the new feedback</returns>
    public Guid AddFeedback(string comment, short score, string userId)
    {
      // TODO validate
      if (_feedbacks.Any(f => f.UserId == userId))
        throw new Exception("User has already submitted his feedback");

      _totalScore += score;
      _feedbacks.Add(Feedback.CreateForShop(Id, comment, score, userId));
      _setAverageScore();
     
      return _feedbacks.Last().Id;
    }

    /// <summary>
    /// Update an existing feedback
    /// </summary>
    /// <param name="feedbackId">Id of the feedback to update</param>
    /// <param name="score">New score to set</param>
    public void UpdateFeedback(Guid feedbackId, short score)
    {
      // TODO validate
      var fb = _feedbacks.FirstOrDefault(f => f.Id == feedbackId);
      _totalScore += (score - fb.Score);
      fb.Score = score;
      _setAverageScore();
    }

    #endregion

    #region Internals

    /// <summary>
    /// Set the average score of the shop
    /// </summary>
    void _setAverageScore()
    {
      AverageScore = (float) _totalScore / _feedbacks.Count;
    }

    #endregion
  }
}
