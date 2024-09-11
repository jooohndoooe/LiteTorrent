namespace LiteTorrent.AppConfig
{
    public class SimpleAppConfiguration : IAppConfiguration
    {
        public Task<AppConfiguration> GetAppConfiguration()
        {
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
}
