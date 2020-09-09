using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace BMS_dotnet_WebApplication.Service
{
    public static class ServiceHostModule
    {
        public static IServiceCollection AddServiceHostModule(this IServiceCollection services)
        {
            services.RegisterAwsModule();
            services.RegisterLibraryBusinessLayerModule();
            return services;
        }
    }
}