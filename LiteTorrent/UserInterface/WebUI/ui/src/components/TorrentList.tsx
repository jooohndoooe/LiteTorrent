import styles from './TorrentList.module.css';
import { ITorrent } from "./api";
import { TorrentRow } from "./TorrentRow";
import { TorrentDetails } from './TorrentDetails';

interface TorrentListProps {
    selectedId: number;
    setSelectedId: Function;
    torrents: ITorrent[];
}

export function TorrentList(props: TorrentListProps) {
    const torrentList = props.torrents.map(_torrent =>
        <div key={_torrent.id}>
            <TorrentRow torrent={_torrent} selectedId={props.selectedId} setSelectedId={props.setSelectedId} />
            <TorrentDetails torrent={_torrent} selectedId={props.selectedId}/>
        </div>
    )

    return (<div className={styles['torrent-list']}>{torrentList}</div>);
}