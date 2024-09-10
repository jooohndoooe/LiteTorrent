import styles from './TorrentList.module.css';
import { Torrent } from "./api";
import { TorrentRow } from "./TorrentRow";

interface TorrentListProps {
    selectedId: number;
    setSelectedId: Function;
    torrents: Torrent[];
}

export function TorrentList(props: TorrentListProps) {
    const torrentList = props.torrents.map(_torrent =>
        <div key={_torrent.id}>
            <TorrentRow torrent={_torrent} selectedId={props.selectedId} setSelectedId={props.setSelectedId} />
        </div>
    )

    return (<div className={styles['torrent-list']}>{torrentList}</div>);
}

export default TorrentList;