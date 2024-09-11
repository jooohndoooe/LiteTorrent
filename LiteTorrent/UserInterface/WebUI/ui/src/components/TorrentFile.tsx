import styles from './TorrentFile.module.css';
import { ITorrentFile } from "./api";

interface TorrentFileProps{
    torrentFile:ITorrentFile;
}

export function TorrentFile(props: TorrentFileProps){

    var progressColor = "#bcb7ad";
    if (props.torrentFile.completion < 40) {
        progressColor = "#ff0000";
    }
    else if (props.torrentFile.completion < 70) {
        progressColor = "#ffa500";
    }
    else {
        progressColor = "#2ecc71";
    }

    return (
        <div className={styles['torrent-file']}>
            <div className={styles.title}>
                {props.torrentFile.name}
            </div>
            <div className={styles.progressbar}>
                <div className={styles['progressbar-fill']} style={{ width: `${props.torrentFile.completion}%`, backgroundColor: progressColor }}>
                </div>
            </div>
            <div className={styles['progress-label']}>
                {Math.round(props.torrentFile.completion * 100) / 100}%
            </div>
        </div>
    );
}