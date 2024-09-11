namespace LiteTorrent.TorrentManagerServices
{
    public interface ITorrentManager
    {
        Task AddTorrent(byte[] torrentBytes);
        Task RemoveTorrent(int id);
    }
}
