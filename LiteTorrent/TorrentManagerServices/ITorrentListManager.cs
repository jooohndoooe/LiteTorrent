using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent.TorrentManagerServices
{
    public interface ITorrentListManager
    {
        Task<List<TorrentInfo>> GetTorrentList();
    }
}
