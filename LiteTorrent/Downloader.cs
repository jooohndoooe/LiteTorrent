using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Connections;

namespace LiteTorrent
{
    class Downloader
    {
        ClientEngine Engine;
        bool isActive;
        private readonly AppConfiguration appConfiguration;

        public Downloader(ClientEngine engine, AppConfiguration appConfiguration)
        {
            this.Engine = engine;
            this.isActive = false;
            this.appConfiguration = appConfiguration;
        }

        public async Task DownloadAsync()
        {
            isActive = true;

            foreach (string file in Directory.GetFiles(appConfiguration.TorrentPath))
            {
                if (file.EndsWith(".torrent")){
                    var settingsBuilder = new TorrentSettingsBuilder
                    {
                        MaximumConnections = 60,
                    };
                    TorrentManager manager = await Engine.AddAsync(file, appConfiguration.DownloadPath, settingsBuilder.ToSettings());
                }
            }

            if (Engine.Torrents.Count == 0) {
                Console.WriteLine("No torrents found");
                return;
            }

            foreach (TorrentManager manager in Engine.Torrents) { 
                await manager.StartAsync();
            }

            while(!isDownloaded())
            {
                Console.Clear();
                ToConsole();
                await Task.Delay(1000);
            }
            isActive = false;
        }

        private bool isDownloaded() {
            foreach (TorrentManager manager in Engine.Torrents)
            {
                foreach (var file in manager.Files) {
                    if ((int)file.BitField.PercentComplete != 100) {
                        return false;
                    } 
                }
            }
            return true;
        }

        private void ToConsole()
        {
            if (isActive)
            {
                foreach (TorrentManager manager in Engine.Torrents)
                {
                    TorrentInfo info = new TorrentInfo(manager);
                    info.ToConsole();
                }
            }
            else {
                Console.WriteLine();
                Console.WriteLine("Downloader not active");
            }
        }
    }
}