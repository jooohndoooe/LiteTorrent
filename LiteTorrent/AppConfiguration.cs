using Microsoft.Extensions.DependencyInjection;
using MonoTorrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent
{
    public interface IAppConfiguration
    {
        Task<AppConfiguration> GetAppConfiguration();
    }

    public class SimpleAppConfiguration : IAppConfiguration
    {
        public Task<AppConfiguration> GetAppConfiguration()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            var torrentPath = Path.Combine(Environment.CurrentDirectory, "Torrents");
            if (!Directory.Exists(torrentPath))
            {
                Directory.CreateDirectory(torrentPath);
            }

            var downloadPath = Path.Combine(Environment.CurrentDirectory, "Downloads");
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            return Task.FromResult(new AppConfiguration { TorrentPath = torrentPath, DownloadPath = downloadPath });
        }
    }

    public class AppConfiguration
    {
        public string TorrentPath { get; set; } = string.Empty;
        public string DownloadPath { get; set; } = string.Empty;
    }

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
