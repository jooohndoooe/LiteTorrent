import { useEffect, useState } from "react";
import { loadTorrents, Torrent } from "./api";
import TorrentList from "./TorrentList";

interface TorrentListContinerProps {
    selectedId: number;
    setSelectedId: Function;
}

export function TorrentListContainer(props: TorrentListContinerProps) {
    const [torrents, setTorrents] = useState(new Array<Torrent>());

    useEffect(
        () => {
            async function loadState() {
                const result = await loadTorrents();
                setTorrents(result);
            }
            loadState();
            setInterval(loadState, 1000);
        }, []);

    return (<TorrentList selectedId={props.selectedId} setSelectedId={props.setSelectedId} torrents={torrents} />);
}

export default TorrentListContainer;