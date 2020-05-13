namespace ElecronicsStore.DB.Models
{
    public class Order_Product_Amount
    {
        public long? Id { get; set; }
        public ProductShortModel Product { get; set; }
        public long? OrderId { get; set; }
        public int Amount { get; set; }
    }
}
