export class TorrentFile{
    completion: number;
    name: string;

    constructor(){
        this.completion = -1;
        this.name = "undefined";
    }
}

export class Torrent{
    id: number;
    files: TorrentFile[];
    name: string;
    state: string;
    totalCompletion: number;

    constructor(){
        this.id = -1;
        this.files = new Array<TorrentFile>();
        this.name = "undefined";
        this.state = "undefined";
        this.totalCompletion = -1;
    }
}



export async function loadTorrents():Promise<Torrent[]> {
    const result = await (await fetch("/api/torrent")).json();
    return result;
}