namespace LiteTorrent.TorrentManagerServices
{
    public interface ITorrentListManager
    {
        Task<List<TorrentInfo>> GetTorrentList();
    }
}
