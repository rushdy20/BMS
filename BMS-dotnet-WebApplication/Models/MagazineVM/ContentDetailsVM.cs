using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class ContentDetailsVM
    {
     public  MagazineContent Content { get; set; }
     public string MagazineCoverImage { get; set; }

     public List<OtherContent> OtherContents { get; set; }

    }

    public class OtherContent
    {
        public string Heading { get; set; }
        public string Id { get; set; }
        public string Image { get; set; }
        public string FolderName { get; set; }
    }
}
