export interface TorrentFile{
    completion: number;
    name: string;
}

export interface Torrent{
    files: TorrentFile[];
    name: string;
    state: string;
    totalCompletion: number;
}

export async function loadTorrents():Promise<Torrent[]> {
    const result = await (await fetch("/api/torrent")).json();
    return result;
}