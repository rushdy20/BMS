using System;
using Microsoft.Extensions.Caching.Memory;

namespace BMS_dotnet_WebApplication.Service
{
    public class CacheManager : _ICacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void RemoveCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T getItemCallBack)
        {
            var item = getItemCallBack;
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTimeOffset.Now.AddDays(30);
            cacheEntryOptions.Priority = CacheItemPriority.High;
            _cache.Set(cacheKey, item, cacheEntryOptions);
        }

        public T Get<T>(string cacheKey)
        {
           return _cache.Get<T>(cacheKey);
        }
    }
}