using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent.AppConfig
{
    public interface IAppConfiguration
    {
        Task<AppConfiguration> GetAppConfiguration();
    }
}
