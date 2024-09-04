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
                <div className = {styles['progressbar-fill']} style = {{width: `${progress}%`, backgroundColor: getColor(progress)}}>
                </div>
            </div>
            <div className = {styles['progress-label']}>
                {progress}%
            </div>
        </div>
    )
}

export default ProgressBar;