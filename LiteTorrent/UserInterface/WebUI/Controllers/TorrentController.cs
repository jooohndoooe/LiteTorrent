using LiteTorrent.TorrentManagerServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LiteTorrent.UserInterface.WebUI.Controllers
{
    public class TorrentController : ControllerBase
    {
        private ITorrentListManager torrentListManager;
        public TorrentController(ITorrentListManager torrentListManager)
        {
            this.torrentListManager = torrentListManager;
        }

        [HttpGet, Route("api/torrent")]
        public async Task<IActionResult> GetTorrentList()
        {
            var torrents = await torrentListManager.GetTorrentList();
            return Ok(torrents);
        }

        [HttpGet, Route("api/health")]
        public async Task<IActionResult> Health()
        {
            return Ok("Healthy");
        }
    }
}
