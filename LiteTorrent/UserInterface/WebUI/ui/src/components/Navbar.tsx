import styles from './Navbar.module.css';

interface INavbar {
    selectedId: number;
}

const Navbar: React.FC<INavbar> = (navbarInfo: INavbar) => {
    const remove = () => {
        if (navbarInfo.selectedId >= 0) {
            console.log('start');
            fetch('/api/torrent/' + navbarInfo.selectedId, { method: 'DELETE' });
            console.log('end');
        }
    }

    return (
        <div className={styles.navbar}>
            <div className={styles['logo-container']}>
                <img src={require('../logo.png')} height="20px" />
                iteTorrent
            </div>
            <button className={styles['button-container']}>
                Add Torrent
            </button>
            <button className={styles['button-container']} onClick={remove}>
                Remove Torrent
            </button>
        </div>
    )
}

export default Navbar;