using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MonoTorrent;
using MonoTorrent.Client;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using LiteTorrent.AppConfig;
using System.Runtime.InteropServices;
using LiteTorrent.TorrentManagerServices;
using LiteTorrent.UserInterface.ConsoleUI;
using LiteTorrent.UserInterface;
using LiteTorrent.UserInterface.WebUI;


const int httpListeningPort = 55125;

var services = new ServiceCollection();
/*services.AddSingleton(
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
services.AddSingleton<TorrentManagerService>();*/
services.AddKeyedSingleton<IUserInterface, ConsoleUserInterface>("cli");
services.AddKeyedSingleton<IUserInterface, WebUserInterface>("web");
await using var provider = services.BuildServiceProvider();

var consoleUI = provider.GetRequiredKeyedService<IUserInterface>("web");
await consoleUI.Run();