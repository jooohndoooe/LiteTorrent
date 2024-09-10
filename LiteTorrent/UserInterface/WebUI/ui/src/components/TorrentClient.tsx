import Navbar from "./Navbar";
import styles from "./TorrentClient.module.css";
import TorrentListContainer from "./TorrentListContainer";

interface TorrentClientProps {
    selectedId: number;
    setSelectedId: Function;
}

function TorrentClient(props: TorrentClientProps) {
    return (
        <div className={styles['torrent-client']}>
            <div className={styles.navbar} onClick={() => { props.setSelectedId(-1); }}>
                <Navbar selectedId={props.selectedId} />
            </div>
            <div className={styles.progressbar}>
                <TorrentListContainer selectedId={props.selectedId} setSelectedId={props.setSelectedId} />
            </div>
        </div>
    );
}

export default TorrentClient;