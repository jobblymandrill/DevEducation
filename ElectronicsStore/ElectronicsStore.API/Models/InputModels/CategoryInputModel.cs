using System;

namespace ElectronicsStore.API.Models.InputModels
{
    public class CategoryInputModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public CategoryInputModel ParentCategory { get; set; }
    }
}
