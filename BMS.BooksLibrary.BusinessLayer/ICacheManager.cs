using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.BooksLibrary.BusinessLayer
{
 public interface ICacheManager
    {
        void RemoveCache(string cacheKey);
        void Set<T>(string cacheKey, T getItemCallBack);
        T Get<T>(string cacheKey);
    }
}
