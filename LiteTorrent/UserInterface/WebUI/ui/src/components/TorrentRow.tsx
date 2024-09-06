import React, { useState } from "react";
import styles from './TorrentRow.module.css';
import { ITorrent } from "./api";

interface ITorrentRow {
    torrent: ITorrent;
    selectedId: number;
    setSelectedId: Function;
}

export const TorrentRow: React.FC<ITorrentRow> = (torrentRowInfo: ITorrentRow) => {
    var torrent = torrentRowInfo.torrent;
    const [rowbBackgroundColor, setRowBackgroundColor] = useState("#ffffff");

    const handleMouseEnter = () => {
        setRowBackgroundColor("#d0d0d0");
    }
    const handleMouseLeave = () => {
        setRowBackgroundColor("#ffffff");
    }
    
    var progressColor = "#bcb7ad";
    if (torrent.totalCompletion < 40) {
        progressColor = "#ff0000";
    }
    else if (torrent.totalCompletion < 70) {
        progressColor = "ffa500";
    }
    else {
        progressColor = "2ecc71";
    }


    // console.log(torrentRowInfo.selectedId, torrent.id);
    var borderStyle = "none";
    if (torrentRowInfo.selectedId == torrent.id) {
        borderStyle = "solid";
    }

    const setSelected = () => {
        torrentRowInfo.setSelectedId(torrent.id);
    }

    return (
        <div className={styles.torrent} onMouseOver={handleMouseEnter} onMouseOut={handleMouseLeave} onClick={setSelected} style={{ backgroundColor: rowbBackgroundColor, borderStyle: borderStyle }}>
            <div className={styles.title}>
                {(torrent.id + 1).toString() + ". " + torrent.name}
            </div>
            <div className={styles.progressbar}>
                <div className={styles['progressbar-fill']} style={{ width: `${torrent.totalCompletion}%`, backgroundColor: progressColor }}>
                </div>
            </div>
            <div className={styles['progress-label']}>
                {torrent.totalCompletion}%
            </div>
        </div>
    );
}

