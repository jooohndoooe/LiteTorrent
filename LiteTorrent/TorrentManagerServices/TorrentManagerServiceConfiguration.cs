namespace LiteTorrent.TorrentManagerServices
{
    public static class TorrentManagerServiceConfiguration
    {
        public static void AddTorrentManager(this IServiceCollection services) {
            services.AddSingleton<ITorrentEngine>(provider => provider.GetRequiredService<TorrentManagerService>());
            services.AddSingleton<ITorrentManager>(provider => provider.GetRequiredService<TorrentManagerService>());
            services.AddSingleton<ITorrentListManager>(provider => provider.GetRequiredService<TorrentManagerService>());
            services.AddSingleton<TorrentManagerService>();
        }
    }
}
