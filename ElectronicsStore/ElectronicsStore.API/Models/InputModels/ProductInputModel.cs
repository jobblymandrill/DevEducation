using ElecronicsStore.DB.Models;

namespace ElectronicsStore.API.Models.InputModels
{
    public class ProductInputModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public CategoryInputModel Category { get; set; }
    }
}
