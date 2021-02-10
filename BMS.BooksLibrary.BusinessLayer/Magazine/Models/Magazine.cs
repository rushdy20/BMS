using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.BusinessLayer.Magazine.Models
{
  public  class Magazine
    {
        public string MagazineId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string FolderName { get; set; }
        public List<MagazineContent> Contents { get; set; }
        public bool IsLive { get; set; }
    }
}
