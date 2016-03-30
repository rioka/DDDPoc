using System;
using System.Linq;
using DDDPoc.Application.InputModels;
using DDDPoc.Application.ViewModels;
using DDDPoC.Domain;
using DDDPoC.Domain.Models;
using DDDPoc.Domain.Repositories;

namespace DDDPoc.Application
{
  public class MerchantService
  {
    readonly IMerchants _merchants;
    readonly IUnitOfWork _uow;

    public MerchantService(IUnitOfWork uow, IMerchants merchants)
    {
      _merchants = merchants;
      _uow = uow;
    }

    public MerchantViewModel Get(Guid id)
    {
      var m = _merchants.Get(id);
      return  new MerchantViewModel(){
        Id= m.Id,
        Name = m.Name
      };
    }

    public void Add(MerchantInputModel model)
    {
      var merchant = new Merchant(model.Id, model.Name);
      _merchants.Add(merchant);
      _uow.Commit();
    }

    public void AddShop(ShopInputModel model)
    {
      var merchant = _merchants.Get(model.MerchantId);
      merchant.AddShop(model.Name);
      _uow.Commit();
    }

    public void AddFeedback(FeedbackInputModel model)
    {
      var merchant = _merchants.FindOwnerOfShop(model.ShopId);
      var shop = merchant.Shops.First(s => s.Id == model.ShopId);
      shop.AddFeedback(model.Comment, model.Score, model.UserId);
      _uow.Commit();
    }
  }
}
