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

        public Downloader(ClientEngine engine)
        {
            this.Engine = engine;
            this.isActive = false;
        }

        public async Task DownloadAsync()
        {
            isActive = true;
            Directory.SetCurrentDirectory(@"..\..\..");

            string torrents = Path.Combine(Environment.CurrentDirectory, "Torrents");
            string downloads = Path.Combine(Environment.CurrentDirectory, "Downloads");

            if (!Directory.Exists(torrents))
            {
                Directory.CreateDirectory(torrents);
            }
            if (!Directory.Exists(downloads))
            {
                Directory.CreateDirectory(downloads);
            }

            foreach (string file in Directory.GetFiles(torrents))
            {
                if (file.EndsWith(".torrent")){
                    var settingsBuilder = new TorrentSettingsBuilder
                    {
                        MaximumConnections = 60,
                    };
                    TorrentManager manager = await Engine.AddAsync(file, downloads, settingsBuilder.ToSettings());
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