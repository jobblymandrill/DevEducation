using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System;
using System.Globalization;

namespace ElectronicsStore.API.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductOutputModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category
                {
                    Name = src.Category.Name,
                    ParentCategory =
                new Category { Name = src.Category.ParentCategory.Name }
                }))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TradeMark, opt => opt.MapFrom(src => src.TradeMark));
            CreateMap<Category, CategoryOutputModel>()
            .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory.Name));
            CreateMap<CategoryWithNumber, CategoryWithNumberOutputModel>();
            CreateMap<ProductWithCity, ProductWithCityOutputModel>();
            CreateMap<PeriodInputModel, Period>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => DateTime.ParseExact(src.StartDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => DateTime.ParseExact(src.EndDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)));
            CreateMap<FilialWithIncome, FilialWithIncomeOutputModel>();
            CreateMap<IncomeByIsForeignCriteria, IncomeByIsForeignCriteriaOutputModel>();
            CreateMap<OrderInputModel, Order>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateTime, "dd.mm.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<Order, OrderOutputModel>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToString()))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<Order_Product_AmountInputModel, Order_Product_Amount>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new ProductShortModel
                {
                    Id = (long)src.Product.Id,
                    Name = null,
                    Price = src.Product.Price
                }));
            CreateMap<Order_Product_Amount, Order_Product_AmountOutputModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new ProductShortOutputModel
                {
                    Name = src.Product.Name,
                    Price = src.Product.Price
                }));
        }
    }
}
