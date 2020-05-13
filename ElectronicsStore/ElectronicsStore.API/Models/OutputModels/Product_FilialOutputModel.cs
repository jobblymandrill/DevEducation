namespace ElectronicsStore.API.Models.OutputModels
{
    public class Product_FilialOutputModel
    {
        public int Id { get; set; }
        public ProductOutputModel Product { get; set; }
        public FilialOutputModel Filial { get; set; }
        public int Amount { get; set; }
    }
}
