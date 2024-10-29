using LiteTorrent.UserInterface.ConsoleUI;
using LiteTorrent.UserInterface;
using LiteTorrent.UserInterface.WebUI;
using LiteTorrent.TorrentManagerServices;
using MonoTorrent.Client;
using LiteTorrent.AppConfig;
using System.Net;


const int httpListeningPort = 55125;

var services = new ServiceCollection();

services.AddAppSettings();
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
services.AddSingleton<TorrentManagerService>();
services.AddKeyedSingleton<IUserInterface, ConsoleUserInterface>("cli");
services.AddKeyedSingleton<IUserInterface, WebUserInterface>("web");
await using var provider = services.BuildServiceProvider();


var ui = provider.GetRequiredKeyedService<IUserInterface>(args[0]);
await ui.Run();