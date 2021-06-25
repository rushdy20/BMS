using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
  public  interface IErrorLog
    {
        Task<bool> Error(string errorMsg);
    }
}
