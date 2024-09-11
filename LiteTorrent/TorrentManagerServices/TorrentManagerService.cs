using MonoTorrent;
using MonoTorrent.Client;
using LiteTorrent.AppConfig;

namespace LiteTorrent.TorrentManagerServices
{
    public class TorrentManagerService : ITorrentManager, ITorrentListManager, ITorrentEngine
    {
        private ClientEngine engine;
        private AppConfiguration appConfiguration;
        private Task initializationTask;

        public TorrentManagerService(ClientEngine engine, AppConfiguration appConfiguration)
        {
            this.engine = engine;
            this.appConfiguration = appConfiguration;
            this.initializationTask = AddExistingTorrents();
        }

        public async Task AddTorrent(byte[] torrentBytes)
        {
            await initializationTask;
            Torrent torrent = Torrent.Load(torrentBytes);
            var manager = await engine.AddAsync(torrent, appConfiguration.DownloadPath);
            await manager.StartAsync();
            File.WriteAllBytes(appConfiguration.TorrentPath + @"\" + manager.Name + ".torrent", torrentBytes);
        }

        public async Task RemoveTorrent(int id)
        {
            await initializationTask;
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
                try
                {
                    await AddTorrentPrivate(torrentBytes);
                }catch (Exception e) { }
            }
        }

        public async Task<List<TorrentInfo>> GetTorrentList()
        {
            await initializationTask;
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
            for (int i = 0; i < torrentList.Count; i++) {
                torrentList[i].SetId(i);
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
