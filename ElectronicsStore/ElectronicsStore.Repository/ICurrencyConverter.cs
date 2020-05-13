using ElectronicsStore.Core;

namespace ElecronicsStore.API
{
    public interface ICurrencyConverter
    {
        decimal ConvertProductPrice(decimal price, CurrencyTypeEnum type = 0);
    }
}