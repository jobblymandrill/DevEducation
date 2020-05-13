namespace ElectronicsStore.API.Models.InputModels
{
    public class Order_Product_AmountInputModel
    {
        public long? Id { get; set; }
        public ProductShortInputModel Product { get; set; }
        public long? OrderId { get; set; }
        public int Amount { get; set; }
    }
}
