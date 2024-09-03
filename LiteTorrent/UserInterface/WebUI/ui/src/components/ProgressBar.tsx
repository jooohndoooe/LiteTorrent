import React, {useEffect, useState} from "react";
import styles from './ProgressBar.module.css';
import { loadTorrents } from "./api";

const ProgressBar: React.FC<{}> = () => {
    const[progress, setProgress] = useState(0);

    useEffect(
        () => {
            async function loadState(){
                const result = await loadTorrents();
                setProgress(result[0].totalCompletion);
            }
            loadState();
        }, []);


    // return(
    //     <div>
    //         {
    //             torrents.map()
    //         }
    //     </div>
    // )

    return(
        <div>
            <div className = {styles.progressbar}>
                <div className = {styles['progressbar-fill']}>
                </div>
            </div>
            <div className = {styles['progress-label']}>
                {progress}%
            </div>
        </div>
    )
}

export default ProgressBar;