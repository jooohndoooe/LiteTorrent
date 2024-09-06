import {useState} from "react";
import Navbar from "./components/Navbar";
import TorrentList from "./components/TorrentList";
import styles from "./App.module.css";

function App() {
    const [selectedId, setSelectedId] = useState(-1);


    return (
        <div className={styles.app}>
            <div className={styles.navbar} onClick={() => {setSelectedId(-1);}}>
                <Navbar selectedId = {selectedId}/>
            </div>
            <div className={styles.progressbar}>
                <TorrentList selectedId = {selectedId} setSelectedId={setSelectedId}/>
            </div>
        </div>
    );
}

export default App;
