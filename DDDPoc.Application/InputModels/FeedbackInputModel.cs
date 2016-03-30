using System;

namespace DDDPoc.Application.InputModels
{
  public class FeedbackInputModel
  {
    public Guid ShopId { get; set; }

    public string Comment { get; set; }

    public short  Score { get; set; }

    public string UserId { get; set; }
  }
}
