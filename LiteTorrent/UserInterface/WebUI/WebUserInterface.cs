using LiteTorrent.TorrentManagerServices;
using MonoTorrent.Client;
using System.Net;
using LiteTorrent.AppConfig;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace LiteTorrent.UserInterface.WebUI
{
    internal class WebUserInterface : IUserInterface
    {
        public async Task Run()
        {
            const int httpListeningPort = 55125;

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                WebRootPath = "C:\\Users\\ustin\\source\\repos\\LiteTorrent\\LiteTorrent\\UserInterface\\WebUI\\ui\\build\\"
            });
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

            builder.Services.AddHostedService<TorrentHostedService>();
            builder.Services.AddSingleton(provider => new ClientEngine(provider.GetRequiredService<EngineSettingsBuilder>().ToSettings()));
            builder.Services.AddAppSettings();
            builder.Services.AddTorrentManager();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
