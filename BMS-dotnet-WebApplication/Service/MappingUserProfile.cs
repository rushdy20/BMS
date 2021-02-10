using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BooksLibrary.BusinessLayer.Models;
using BMS.BusinessLayer.Magazine.Models;
using BMS.BusinessLayer.Users.Models;
using BMS_dotnet_WebApplication.Models.LibraryVM;
using BMS_dotnet_WebApplication.Models.MagazineVM;
using BMS_dotnet_WebApplication.Models.UserVM;
using Microsoft.Extensions.DependencyInjection;

namespace BMS_dotnet_WebApplication.Service
{
    public class MappingUserProfile :Profile
    {
        public MappingUserProfile()
        {
            CreateMap<SearchForBookModel, SearchForBookVM>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<BooksCategoryModel, RegisterBookVM>()
                .ForMember(dest => dest.SelectedCategory, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.SelectedCategoryText, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<BookModel, RegisterBookVM>()
                .ReverseMap();

            CreateMap<RegistrationModel, SignUpViewModel>()
                .ReverseMap();

            CreateMap<MagazineCategoriesVM, MagazineCategory>()
                .ReverseMap();

            CreateMap<Magazine, RemoveMagazineVM>();
        }
    }
}
