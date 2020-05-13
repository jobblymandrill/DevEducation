namespace ElecronicsStore.DB.Models
{
    public class Product_Filial
    {
        public int Id { get; set; }
        public Filial Filial { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
