import React, {useEffect, useState} from "react";
import styles from './ProgressBar.module.css';
import { loadTorrents, Torrent} from "./api";

const ProgressBar: React.FC<{}> = () => {
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

    const torrentList = torrents.map(torrent => 
        <div key = {torrent.id} className = {styles.torrent}>
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
    )

    return(<div className={styles['torrent-list']}>{torrentList}</div>);
}

export default ProgressBar;