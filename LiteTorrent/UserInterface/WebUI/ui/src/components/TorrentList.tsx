import React, {useEffect, useState} from "react";
import styles from './TorrentList.module.css';
import { loadTorrents, Torrent} from "./api";
import { TorrentRow } from "./Torrent";

const TorrentList: React.FC<{}> = () => {
    const t = new Torrent();
    const[torrents, setTorrents] = useState(new Array<Torrent>());

    useEffect(
        () => {
            async function loadState(){
                const result = await loadTorrents();
                setTorrents(result);
            }
            loadState();
        }, []);

        const torrentList = torrents.map(_torrent => 
            <div key = {_torrent.id}>
                <TorrentRow torrent={_torrent}/>
            </div>
        )

        return(<div className={styles['torrent-list']}>{torrentList}</div>);
    }

export default TorrentList;