using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent
{
    internal interface ITorrentListManager
    {
        List<TorrentInfo> GetTorrentList();
    }
}
