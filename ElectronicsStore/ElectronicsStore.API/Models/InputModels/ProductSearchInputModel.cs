using System;

namespace ElectronicsStore.API.Models.InputModels
{
    public class ProductSearchInputModel
    {
        public long? Id { get; set; }
        public string IdOperation { get; set; }
        public string Name { get; set; }
        public string NameOperation { get; set; }
        public decimal? Price { get; set; }
        public string PriceOperation { get; set; }
        public string TradeMark { get; set; }
        public string TradeMarkOperation { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryIdOperation { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryIdOperation { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameOperation { get; set; }
        public string ParentCategoryName { get; set; }
        public string ParentCategoryNameOperation { get; set; }
        public decimal? PriceEnd { get; set; }
    }
}
