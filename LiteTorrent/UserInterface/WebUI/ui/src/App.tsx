import React from 'react';
import Navbar from './components/Navbar';
import TorrentList from './components/TorrentList';
import styles from './App.module.css';

function App() {
  return (
    <div className={styles.app}>
      <div className={styles.navbar}>
        <Navbar/>
      </div>
      <div className={styles.progressbar}>
        <TorrentList/>
      </div>
    </div>
  );
}

export default App;
