using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS_dotnet_WebApplication.Service
{
    public interface ICacheManager
    {
        void RemoveCache(string cacheKey);
        void Set<T>(string cacheKey, T getItemCallBack);
        T Get<T>(string cacheKey);
    }
}
