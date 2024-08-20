using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;

namespace LiteTorrent
{
    internal interface ITorrentManager
    {
        Task AddTorrent(Torrent torrent);
        Task RemoveTorrent(Torrent torrent);
    }
}
