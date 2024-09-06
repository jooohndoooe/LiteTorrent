import React, {useEffect, useState} from "react";
import styles from './Torrent.module.css';
import {ITorrent} from "./api";

interface ITorrentInfo{
    torrent: ITorrent;
}

export const TorrentRow: React.FC<ITorrentInfo> = (torrentInfo: ITorrentInfo) => {
    var torrent = torrentInfo.torrent;
    const [backgroundColor, setBackgroundColor] = useState("#ffffff");

    const handleMouseEnter = () => {
        setBackgroundColor("#d0d0d0");
    }
    
    const handleMouseLeave = () => {
        setBackgroundColor("#ffffff");
    }

    var getColor = (p: number) => {
        if(p < 40){
            return "#ff0000";
        }
        else if(p < 70){
            return "ffa500";
        }
        else{
            return "2ecc71";
        }
    };

    return(
        <div className={styles.torrent} onMouseOver={handleMouseEnter} onMouseOut={handleMouseLeave} style={{backgroundColor: backgroundColor}}>
            <div className={styles.title}>
                {(torrent.id + 1).toString() + ". " + torrent.name}
            </div>
            <div className = {styles.progressbar}>
                <div className = {styles['progressbar-fill']} style = {{width: `${torrent.totalCompletion}%`, backgroundColor: getColor(torrent.totalCompletion)}}>
                </div>
            </div>
            <div className = {styles['progress-label']}>
                {torrent.totalCompletion}%
            </div>
        </div>
    );
}

