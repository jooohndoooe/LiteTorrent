using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteTorrent.TorrentManagerServices;

namespace LiteTorrent.UserInterface
{
    internal interface IUserInterface
    {
        Task Run();
    }
}
