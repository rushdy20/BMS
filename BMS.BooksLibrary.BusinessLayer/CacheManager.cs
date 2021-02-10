using System;
using Microsoft.Extensions.Caching.Memory;

namespace BMS.BooksLibrary.BusinessLayer
{
    public class CacheManager : ICacheManager
    {
        private const string EmptyKey = "EmptyKey";
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
            try
            {
                return _cache.Get<T>(cacheKey);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return _cache.Get<T>(EmptyKey);
            }
            
            
        }
    }
}