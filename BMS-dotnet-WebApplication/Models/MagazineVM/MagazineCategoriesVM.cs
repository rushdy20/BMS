using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class MagazineCategoriesVM
    {
        public MagazineCategoryVM AddMagazineCategory { get; set; }
       
        public List<MagazineCategory> ExistingCategories { get; set; }
    }

}
