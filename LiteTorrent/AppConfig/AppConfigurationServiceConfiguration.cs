using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent.AppConfig
{
    public static class AppConfigurationServiceConfiguration
    {
        public static void AddAppSettings(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSingleton<IAppConfiguration, SimpleAppConfiguration>();
            services.AddSingleton(provider => provider.GetRequiredService<IAppConfiguration>().GetAppConfiguration().Result);
        }
    }
}
