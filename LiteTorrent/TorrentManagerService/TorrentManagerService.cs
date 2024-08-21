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

namespace LiteTorrent.TorrentManagerService
{
    internal class TorrentManagerService : ITorrentManager, ITorrentListManager
    {
        ClientEngine Engine { get; set; }
        AppConfiguration AppConfiguration { get; set; }

        public TorrentManagerService(ClientEngine engine, AppConfiguration appConfiguration)
        {
            Engine = engine;
            AppConfiguration = appConfiguration;
        }

        public async Task AddTorrent(byte[] torrentBytes)
        {
            Torrent torrent = Torrent.Load(torrentBytes);
            var manager = await Engine.AddAsync(torrent, AppConfiguration.DownloadPath);
            await manager.StartAsync();
        }

        public async Task RemoveTorrent(byte[] torrentBytes)
        {
            Torrent torrent = Torrent.Load(torrentBytes);
            await Engine.RemoveAsync(torrent);
        }

        public List<TorrentInfo> GetTorrentList()
        {
            List<TorrentInfo> torrentList = new List<TorrentInfo>();
            foreach (var manager in Engine.Torrents)
            {
                TorrentInfo info = new TorrentInfo();
                info.Update(manager);
                torrentList.Add(info);
            }
            return torrentList;
        }

        public bool isRunning()
        {
            return Engine.IsRunning;
        }

        public void toConsole()
        {
            var torrentList = GetTorrentList();
            foreach (var torrentInfo in torrentList)
            {
                torrentInfo.ToConsole();
            }
        }
    }
}
