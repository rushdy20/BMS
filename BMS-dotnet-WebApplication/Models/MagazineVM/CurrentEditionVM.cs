using System.Collections.Generic;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class CurrentEditionVM
    {
        public List<MagazineCategory> ContentCategories { get; set; }
        public Magazine Magazine { get; set; }
    }
}