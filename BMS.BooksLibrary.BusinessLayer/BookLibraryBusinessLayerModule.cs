
using Microsoft.Extensions.DependencyInjection;

namespace BMS.BooksLibrary.BusinessLayer
{
    public static class BookLibraryBusinessLayerModule
    {
        public static IServiceCollection RegisterLibraryBusinessLayerModule(this IServiceCollection services)
        {
            services.AddScoped<IBooksLibraryManager, BooksLibraryManager>();

            return services;
        }
    }
}
