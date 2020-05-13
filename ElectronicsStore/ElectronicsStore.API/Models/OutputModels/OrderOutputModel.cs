using System.Collections.Generic;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class OrderOutputModel
    {
        public long Id { get; set; }
        public string DateTime { get; set; }
        public string FilialCity { get; set; }
        public List<Order_Product_AmountOutputModel> Products { get; set; }
    }
}
