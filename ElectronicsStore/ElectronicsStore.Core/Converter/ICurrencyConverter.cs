using ElectronicsStore.Core;
using System.Threading.Tasks;

namespace ElecronicsStore.Core
{
    public interface ICurrencyConverter
    {
        ValueTask<decimal> GetCurrentExchangeRate(CurrencyTypeEnum type);
    }
}