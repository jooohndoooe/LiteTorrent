using LiteTorrent.TorrentManagerServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LiteTorrent.UserInterface.WebUI.Controllers
{
    [ApiController]
    public class TorrentController : ControllerBase
    {
        private ITorrentListManager torrentListManager;
        private ITorrentManager torrentManager;
        public TorrentController(ITorrentListManager torrentListManager, ITorrentManager torrentManager)
        {
            this.torrentListManager = torrentListManager;
            this.torrentManager = torrentManager;
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

        [HttpDelete, Route("/api/torrent/{id}")]
        public async Task<IActionResult> RemoveTorrent([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await torrentManager.RemoveTorrent(id);
            return NoContent();
        }

        [HttpPost, Route("/api/torrent")]
        public async Task<IActionResult> AddTorrent([FromForm] IFormFile file)
        {
            var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();
            await torrentManager.AddTorrent(bytes);
            return Ok();
        }
    }
}
