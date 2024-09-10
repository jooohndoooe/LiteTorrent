export interface ITorrentFile {
    completion: number;
    name: string;
}

export class TorrentFile implements ITorrentFile {
    completion: number;
    name: string;

    constructor() {
        this.completion = -1;
        this.name = "undefined";
    }
}

export interface ITorrent {
    id: number;
    files: TorrentFile[];
    name: string;
    state: string;
    totalCompletion: number;
}

export class Torrent implements ITorrent {
    id: number;
    files: TorrentFile[];
    name: string;
    state: string;
    totalCompletion: number;

    constructor() {
        this.id = -1;
        this.files = new Array<TorrentFile>();
        this.name = "undefined";
        this.state = "undefined";
        this.totalCompletion = -1;
    }
}

export async function loadTorrents(): Promise<Torrent[]> {
    const result = await (await fetch("/api/torrent")).json();
    return result;
}

export function addTorrent(inputRef: React.RefObject<HTMLInputElement>) {
    if (!inputRef || !inputRef.current) { return; }
    inputRef.current.click();
}

export async function removeTorrent(selectedId: number) {
    if (selectedId >= 0) {
        await fetch('/api/torrent/' + selectedId, { method: 'DELETE' });
    }
}