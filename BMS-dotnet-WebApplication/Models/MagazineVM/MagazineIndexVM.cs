using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class MagazineIndexVM
    {
        public string CurrentEditionName { get; set; }
        public string CurrentEditionImage { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string MagazineId { get; set; }
        public List<MagazineCategory> ContentCategories { get; set; }
        public bool IsLive { get; set; }
    }
}
