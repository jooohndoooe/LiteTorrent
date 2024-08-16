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

Console.OutputEncoding = Encoding.UTF8;

const int httpListeningPort = 55125;
var settingBuilder = new EngineSettingsBuilder
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
};

var engine = new ClientEngine(settingBuilder.ToSettings());
Task task = new Downloader(engine).DownloadAsync();
await task;

/*foreach (var manager in engine.Torrents)
{
    var stoppingTask = manager.StopAsync();
    while (manager.State != TorrentState.Stopped)
    {
        await Task.WhenAll(stoppingTask, Task.Delay(250));
    }
    await stoppingTask;
}*/