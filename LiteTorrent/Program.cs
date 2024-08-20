using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;

using LiteTorrent;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using LiteTorrent.AppConfig;
using System.Runtime.InteropServices;

Console.OutputEncoding = Encoding.UTF8;

const int httpListeningPort = 55125;

var services = new ServiceCollection();
services.AddSingleton(
    new EngineSettingsBuilder
    {
        AllowPortForwarding = true,
        AutoSaveLoadDhtCache = true,
        AutoSaveLoadFastResume = true,
        ListenEndPoints = new Dictionary<string, IPEndPoint> {
                        { "ipv4", new IPEndPoint (IPAddress.Any, 55123) },
                        { "ipv6", new IPEndPoint (IPAddress.IPv6Any, 55123) }
                    },
        DhtEndPoint = new IPEndPoint(IPAddress.Any, 55123),
        HttpStreamingPrefix = $"http://127.0.0.1:{httpListeningPort}/"
    }
    );

services.AddSingleton(provider => new ClientEngine(provider.GetRequiredService<EngineSettingsBuilder>().ToSettings()));
services.AddAppSettings();
services.AddTransient<TorrentManager2>();


await using var provider = services.BuildServiceProvider();


var torrentManager = provider.GetService<TorrentManager2>();
var appConfiguration = provider.GetService<AppConfiguration>();
foreach (var file in Directory.GetFiles(appConfiguration.TorrentPath))
{
    Torrent torrent = Torrent.Load(file);
    await torrentManager.AddTorrent(torrent);
}

while(torrentManager.isRunning())
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
    torrentManager.toConsole();
    await Task.Delay(1000);
}
