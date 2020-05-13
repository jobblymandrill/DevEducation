using System.Collections.Generic;

namespace ElectronicsStore.API.Models.InputModels
{
    public class OrderInputModel
    {
        public long? Id { get; set; }
        public string DateTime { get; set; }//should be removed from here
        public int FilialId { get; set; }
        public string FilialCity { get; set; }
        public List<Order_Product_AmountInputModel> Products { get; set; }
    }
}
