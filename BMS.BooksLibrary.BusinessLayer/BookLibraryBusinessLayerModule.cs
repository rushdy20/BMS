
using BMS.BusinessLayer.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BMS.BooksLibrary.BusinessLayer
{
    public static class BookLibraryBusinessLayerModule
    {
        public static IServiceCollection RegisterLibraryBusinessLayerModule(this IServiceCollection services){

            services.AddScoped<IBooksLibraryManager, BooksLibraryManager>();
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<IUserManager, UserManager>();

            return services;
        }
    }
}
