namespace LiteTorrent.AppConfig
{
    public interface IAppConfiguration
    {
        Task<AppConfiguration> GetAppConfiguration();
    }
}
