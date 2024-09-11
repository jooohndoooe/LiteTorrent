import styles from './TorrentDetails.module.css'
import { ITorrent } from './api';
import { TorrentFile } from './TorrentFile';

interface TorrentDetailsProps {
    torrent: ITorrent;
    selectedId: number;
}

export function TorrentDetails(props: TorrentDetailsProps) {
    var display = `none`;
    if (props.torrent.id == props.selectedId) {
        display = `block`;
    }
    const torrentDetails = props.torrent.files.map(_torrentFile =>
        <div key={_torrentFile.name}>
            <TorrentFile torrentFile={_torrentFile} />
        </div>
    )
    return (<div className={styles['torrent-details']} style={{ display: display }} >{torrentDetails}</div>);
}