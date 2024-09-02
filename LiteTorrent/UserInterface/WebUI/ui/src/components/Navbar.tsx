import React, {useState} from "react";
import styles from './Navbar.module.css';

const Navbar: React.FC<{}> = () => {
    return(
        <div className={styles.navbar}>
            <div className={styles['logo-container']}>
                <img src={require('../logo.png')}  height="20px"/>
                iteTorrent
            </div>
            <button className={styles['button-container']}>
                Add Torrent
            </button>
            <button className={styles['button-container']}>
                Remove Torrent
            </button>
        </div>
    )
}

export default Navbar;