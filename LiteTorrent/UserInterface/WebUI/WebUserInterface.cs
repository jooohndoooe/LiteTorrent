using LiteTorrent.TorrentManagerServices;
using MonoTorrent.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using LiteTorrent.AppConfig;
using System.Runtime.InteropServices;
using LiteTorrent.TorrentManagerServices;
using LiteTorrent.UserInterface.ConsoleUI;
using LiteTorrent.UserInterface;
using LiteTorrent.UserInterface.WebUI;

namespace LiteTorrent.UserInterface.WebUI
{
    internal class WebUserInterface : IUserInterface
    {
        public async Task Run()
        {
            const int httpListeningPort = 55125;

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddSingleton(
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

            builder.Services.AddSingleton(provider => new ClientEngine(provider.GetRequiredService<EngineSettingsBuilder>().ToSettings()));
            builder.Services.AddAppSettings();
            builder.Services.AddSingleton<TorrentManagerService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
