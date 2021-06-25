using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BMS.BusinessLayer.Models
{
  public  class FeedbackModel
    {
        public string FeedbackOn { get; set; }
        public string Name { get; set; }
      
        public string EmailAddress { get; set; }
       
        public string FeedBack { get; set; }
    }
}
