﻿using MonoTorrent;
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
using System.Security.Cryptography.X509Certificates;

namespace LiteTorrent.TorrentManagerServices
{
    internal class TorrentManagerService : ITorrentManager, ITorrentListManager
    {
        private ClientEngine engine;
        private AppConfiguration appConfiguration;
        private Task initialisationTask;

        public TorrentManagerService(ClientEngine engine, AppConfiguration appConfiguration)
        {
            this.engine = engine;
            this.appConfiguration = appConfiguration;
            this.initialisationTask = AddExistingTorrents();
        }

        public async Task AddTorrent(byte[] torrentBytes)
        {
            await initialisationTask;
            Torrent torrent = Torrent.Load(torrentBytes);
            var manager = await engine.AddAsync(torrent, appConfiguration.DownloadPath);
            await manager.StartAsync();
            File.WriteAllBytes(appConfiguration.TorrentPath + @"\" + manager.Name + ".torrent", torrentBytes);
        }

        public async Task RemoveTorrent(int id)
        {
            await initialisationTask;
            if (id < 0 || id >= engine.Torrents.Count)
            {
                throw new Exception("Invalid id");
            }
            string name = (await GetTorrentList())[id].GetName();
            var targetManager = engine.Torrents[0];
            foreach (var manager in engine.Torrents)
            {
                if (manager.Name == name)
                {
                    targetManager = manager;
                }
            }
            await targetManager.StopAsync();
            await engine.RemoveAsync(targetManager);

            foreach (var file in Directory.GetFiles(appConfiguration.TorrentPath))
            {
                if (name + ".torrent" == Path.GetFileName(file))
                {
                    File.Delete(file);

                }
            }
        }

        public async Task AddExistingTorrents()
        {
            foreach (var file in Directory.GetFiles(appConfiguration.TorrentPath))
            {
                byte[] torrentBytes = File.ReadAllBytes(file);
                await AddTorrentPrivate(torrentBytes);
            }
        }

        public async Task<List<TorrentInfo>> GetTorrentList()
        {
            await initialisationTask;
            List<TorrentInfo> torrentList = new List<TorrentInfo>();
            foreach (var manager in engine.Torrents)
            {
                TorrentInfo info = new TorrentInfo();
                info.Update(manager);
                string name = manager.Name;

                int i = 0;
                while (i < torrentList.Count && string.Compare(name, torrentList[i].GetName()) > 0)
                {
                    i++;
                }
                torrentList.Insert(i, info);
            }
            return torrentList;
        }

        public bool IsRunning()
        {
            return engine.IsRunning;
        }

        private async Task AddTorrentPrivate(byte[] torrentBytes)
        {
            Torrent torrent = Torrent.Load(torrentBytes);
            var manager = await engine.AddAsync(torrent, appConfiguration.DownloadPath);
            await manager.StartAsync();
            File.WriteAllBytes(appConfiguration.TorrentPath + @"\" + manager.Name + ".torrent", torrentBytes);
        }
    }
}
