using System;
using System.Linq;
using DDDPoc.Application;
using DDDPoc.Application.InputModels;
using DDDPoC.Domain;
using DDDPoC.Infrastructure.Persistence.EF;
using DDDPoC.Infrastructure.Persistence.EF.Repositories;

namespace DDDPoC.Sample
{
  class Program
  {
    static void Main(string[] args)
    {
      var rgn = new Random();
      
      var context = new PoCContext("DDDPoC");
      var merchants = new Merchants(context);
      var uow = new UnitOfWork(context);
      var service = new MerchantService(uow, merchants);

      var id = CombGuid.Generate();
      var inputModel = new MerchantInputModel(){
        Id = id,
        Name = "Sample " + DateTime.Now
      };
      service.Add(inputModel);

      context.Dispose();

      context = new PoCContext("DDDPoC");
      merchants = new Merchants(context);
      uow = new UnitOfWork(context);
      service = new MerchantService(uow, merchants);

      var viewModel = service.Get(id);
      var shopInputModel = new ShopInputModel(){
        MerchantId = viewModel.Id,
        Name = "Shop " + DateTime.Now
      };
      service.AddShop(shopInputModel);

      var merchantsWithShops = merchants.FindShopsWithFeedback();
      context.Dispose();

      // Get full graph for merchant
      context = new PoCContext("DDDPoC");
      merchants = new Merchants(context);
      uow = new UnitOfWork(context);
      service = new MerchantService(uow, merchants);

      viewModel = service.Get(id);

      var aMerchantWithShops = merchants.FindShopsWithFeedback().First();
      var feedbackInputModel = new FeedbackInputModel(){
        ShopId = aMerchantWithShops.Shops.First().Id,
        Comment = "Quite disappointed",
        Score = (short) rgn.Next(1, 5),
        UserId = "Jo-Smith " + DateTime.Now
      };
      service.AddFeedback(feedbackInputModel);

      aMerchantWithShops = merchants.FindShopsWithFeedback().Last();
      feedbackInputModel = new FeedbackInputModel() {
        ShopId = aMerchantWithShops.Shops.First().Id,
        Comment = "Not so good",
        Score = (short) rgn.Next(1, 5),
        UserId = "Jane-Smith " + DateTime.Now
      };
      service.AddFeedback(feedbackInputModel);
    }
  }
}
