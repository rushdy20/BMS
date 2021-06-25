using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine.Models;
using System.ComponentModel.DataAnnotations;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class ContentDetailsVM
    {
     public  MagazineContent Content { get; set; }
     public string MagazineCoverImage { get; set; }

     public List<OtherContent> OtherContents { get; set; }

     public string ShortBody
     {

         get
         {
             if (this.Content.Body.Length > 100)
             {
               return  this.Content.Body.Substring(100);
             }
             else
             {
                return this.Content.Body;
             }
         }
     }
    }

    public class OtherContent
    {
        public string Heading { get; set; }
        public string Id { get; set; }
        public string Image { get; set; }
        public string FolderName { get; set; }
        public string MagazineId { get; set; }
    }
}
