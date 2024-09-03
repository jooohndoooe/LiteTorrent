import React from 'react';
import Navbar from './components/Navbar';
import ProgressBar from './components/ProgressBar';
import styles from './App.module.css';

function App() {
  return (
    <div className={styles.app}>
      <div className={styles.navbar}>
        <Navbar/>
      </div>
      <div className={styles.progressbar}>
        <ProgressBar/>
      </div>
    </div>
  );
}

export default App;
