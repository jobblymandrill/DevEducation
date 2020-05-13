using ElecronicsStore.DB.Models;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class FilialOutputModel
    {
        public int Name { get; set; }
        public string CountryName { get; set; }
        public bool IsForeign { get; set; }
    }
}
