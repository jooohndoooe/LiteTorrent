using LiteTorrent.TorrentManagerServices;

namespace LiteTorrent.UserInterface.WebUI
{
    public class TorrentHostedService : IHostedService
    {
        private ITorrentEngine torrentEngine;

        public TorrentHostedService(ITorrentEngine torrentEngine)
        {
            this.torrentEngine = torrentEngine;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true) {
                if (!torrentEngine.IsRunning())
                {
                    await Task.Delay(100);
                }
                else {
                    break;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
