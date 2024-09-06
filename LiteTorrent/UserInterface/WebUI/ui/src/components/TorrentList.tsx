import React, { useEffect, useState } from "react";
import styles from './TorrentList.module.css';
import { loadTorrents, Torrent } from "./api";
import { TorrentRow } from "./TorrentRow";

interface ITorrentList {
    selectedId: number;
    setSelectedId: Function;
}

const TorrentList: React.FC<ITorrentList> = (torrentListInfo: ITorrentList) => {
    const t = new Torrent();
    const [torrents, setTorrents] = useState(new Array<Torrent>());

    useEffect(
        () => {
            async function loadState() {
                const result = await loadTorrents();
                setTorrents(result);
            }
            loadState();
        }, []);

    const torrentList = torrents.map(_torrent =>
        <div key={_torrent.id}>
            <TorrentRow torrent={_torrent} selectedId={torrentListInfo.selectedId} setSelectedId={torrentListInfo.setSelectedId} />
        </div>
    )

    return (<div className={styles['torrent-list']}>{torrentList}</div>);
}

export default TorrentList;