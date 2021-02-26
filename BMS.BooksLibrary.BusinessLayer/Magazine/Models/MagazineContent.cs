using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.BusinessLayer.Magazine.Models
{
  public  class MagazineContent
    {
        public string MagazineId { get; set; }
        public string ContentId { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public string MainImage { get; set; }
        public string SubImage1 { get; set; }
        public string SubImage2 { get; set; }
        public string SubImage3 { get; set; }
        public string SubImage4 { get; set; }
        public string SubImage5 { get; set; }
        public string YouTubLink { get; set; }
        public string EnteredBy { get; set; }
        public string AuthoredBy { get; set; } 
        public string EnteredDate { get; set; }
        public string FolderName { get; set; }
        public MagazineCategory Category { get; set; }
        public int Index { get; set; }
    }
}
