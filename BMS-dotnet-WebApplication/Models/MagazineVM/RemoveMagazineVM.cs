using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class RemoveMagazineVM
    {
        public string MagazineId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string FolderName { get; set; }
        public List<MagazineContent> Contents { get; set; }
        public bool IsLive { get; set; }
        public List<string> RemoveContentId { get; set; }
    }
}
