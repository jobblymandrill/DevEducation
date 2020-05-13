namespace ElectronicsStore.API.Models.OutputModels
{
    public class ProductOutputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public CategoryOutputModel Category { get; set; }
    }
}
