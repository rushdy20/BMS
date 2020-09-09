using Microsoft.Extensions.DependencyInjection;

namespace BMS.AWS
{
    public static class AwsModule
    {
        public static IServiceCollection RegisterAwsModule(this IServiceCollection services)
        {
            services.AddScoped<IS3Uploader, S3Uploader>();

            return services;
        }
    }
}