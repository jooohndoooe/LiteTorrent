using MonoTorrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Connections;
using LiteTorrent.AppConfig;
using System.Runtime.InteropServices;

namespace LiteTorrent
{
    internal class TorrentManager2: ITorrentManager, ITorrentListManager
    {
        ClientEngine Engine;
        AppConfiguration AppConfiguration;

        public TorrentManager2(ClientEngine engine, AppConfiguration appConfiguration) { 
            Engine = engine;
            AppConfiguration = appConfiguration;
        }

        public async Task AddTorrent(Torrent torrent)
        {
            var manager = await Engine.AddAsync(torrent, AppConfiguration.DownloadPath);
            await manager.StartAsync();
        }

        public async Task RemoveTorrent(Torrent torrent)
        {
            await Engine.RemoveAsync(torrent);
        }

        public List<TorrentInfo> GetTorrentList()
        {
            List<TorrentInfo> TorrentList = new List<TorrentInfo>();
            foreach (var manager in Engine.Torrents) {
                TorrentInfo info = new TorrentInfo(manager);
                TorrentList.Add(info);
            }
            return TorrentList;
        }

        public bool isRunning() {
            return Engine.IsRunning;
        }

        public void toConsole() { 
            var torrentList = GetTorrentList();
            foreach (var torrentInfo in torrentList) { 
                torrentInfo.ToConsole();
            }
        }
    }
}
