using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BMS.BusinessLayer.Models;

namespace BMS.BusinessLayer
{
  public interface IFeedBack
  {
      Task<bool> CreateFeedBack(FeedbackModel feedback);
      Task<List<FeedbackModel>> ReadFeedBak();

    }
}
