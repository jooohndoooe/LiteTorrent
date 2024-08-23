using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.Eventing.Reader;

namespace LiteTorrent.TorrentManagerServices
{
    public interface ITorrentEngine
    {
        bool IsRunning();
    }
}
