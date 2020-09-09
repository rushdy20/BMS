using Microsoft.Extensions.DependencyInjection;

namespace BMS.Library.BusinessLayer
{
    public static class LibraryBusinessLayerModule
    {
        public static IServiceCollection RegisterLibraryBusinessLayerModule(this IServiceCollection services)
        {
            //services.AddScoped<ITmpServiceManager, TmpServiceManager>();

            return services;
        }
    }
}