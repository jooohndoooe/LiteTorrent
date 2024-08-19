using Microsoft.Extensions.DependencyInjection;
using MonoTorrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent.AppConfig
{
    public class AppConfiguration
    {
        public string TorrentPath { get; set; } = string.Empty;
        public string DownloadPath { get; set; } = string.Empty;
    }
}
