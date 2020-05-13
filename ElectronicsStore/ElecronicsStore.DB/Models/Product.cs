namespace ElecronicsStore.DB.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public Category Category { get; set; }
    }
}
