import React, { useState } from "react";
import styles from './TorrentRow.module.css';
import { ITorrent } from "./api";

interface TorrentRowProps {
    torrent: ITorrent;
    selectedId: number;
    setSelectedId: Function;
}

export function TorrentRow(props: TorrentRowProps) {
    var torrent = props.torrent;

    var progressColor = "#bcb7ad";
    if (torrent.totalCompletion < 40) {
        progressColor = "#ff0000";
    }
    else if (torrent.totalCompletion < 70) {
        progressColor = "#ffa500";
    }
    else {
        progressColor = "#2ecc71";
    }

    var borderStyle = "none";
    if (props.selectedId == torrent.id) {
        borderStyle = "solid";
    }

    const setSelected = () => {
        props.setSelectedId(torrent.id);
    }

    return (
        <div className={styles.torrent} onClick={setSelected} style={{ borderStyle: borderStyle }}>
            <div className={styles.title}>
                {(torrent.id + 1).toString() + ". " + torrent.name}
            </div>
            <div className={styles.progressbar}>
                <div className={styles['progressbar-fill']} style={{ width: `${torrent.totalCompletion}%`, backgroundColor: progressColor }}>
                </div>
            </div>
            <div className={styles['progress-label']}>
                {Math.round(torrent.totalCompletion * 100) / 100}%
            </div>
        </div>
    );
}

