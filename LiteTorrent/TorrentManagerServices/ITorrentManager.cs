using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;

namespace LiteTorrent.TorrentManagerServices
{
    internal interface ITorrentManager
    {
        Task AddTorrent(byte[] torrentBytes);
        Task RemoveTorrent(int id);
    }
}
