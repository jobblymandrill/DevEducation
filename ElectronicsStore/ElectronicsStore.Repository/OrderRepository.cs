using ElecronicsStore.Core;
using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using ElectronicsStore.Core;
using ElectronicsStore.Core.Countries;
using System;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderStorage _orderStorage;
        private ICurrencyConverter _currencyConverter;

        public OrderRepository(IOrderStorage orderStorage, ICurrencyConverter currencyConverter)
        {
            _orderStorage = orderStorage;
            _currencyConverter = currencyConverter;
        }

        public async ValueTask<RequestResult<Order>> AddOrder(Order dataModel)
        {
            if (!Russia.Cities.Contains(dataModel.FilialCity)) { await ConvertPrices(dataModel); }
            var result = new RequestResult<Order>();
            try
            {
                _orderStorage.TransactionStart();
                result.RequestData = await _orderStorage.AddOrder(dataModel);
                _orderStorage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                _orderStorage.TransactioRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask ConvertPrices(Order dataModel)
        {
            decimal rate = 1;
            if (Belorussia.Cities.Contains(dataModel.FilialCity)) { rate = await _currencyConverter.GetCurrentExchangeRate(CurrencyTypeEnum.BelorussianCurrency); }
            if (Ukraine.Cities.Contains(dataModel.FilialCity)) { rate = await _currencyConverter.GetCurrentExchangeRate(CurrencyTypeEnum.UkrainianCurrency); }
            foreach(var item in dataModel.Products)
            {
                item.Product.Price /= rate;
                item.Product.Price = decimal.Round(item.Product.Price, 3);
            }
        }
    }
}
    
