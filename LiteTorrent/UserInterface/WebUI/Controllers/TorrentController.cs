using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent.UserInterface.WebUI.Controllers
{
    public class TorrentController : ControllerBase
    {
        [HttpGet, Route("api/health")]
        public async Task<IActionResult> Health()
        {
            return Ok("Healthy");
        }
    }
}
