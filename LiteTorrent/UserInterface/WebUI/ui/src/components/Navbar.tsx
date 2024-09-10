import { useRef } from 'react';
import styles from './Navbar.module.css';
import { addTorrent, removeTorrent } from "./api";

interface NavbarProps {
    selectedId: number;
}

export function Navbar(props: NavbarProps) {
    const inputRef = useRef<HTMLInputElement>(null);

    const handleOnChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const files = e.target.files;
        if (!files) {
            return;
        }
        const file = files[0];
        const formData = new FormData();
        formData.append('file', file);
        await fetch('/api/torrent', { method: 'POST', body: formData });
    }

    const add = (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        addTorrent(inputRef);
    }

    const remove = async () => {
        removeTorrent(props.selectedId);
    }

    return (
        <div className={styles.navbar}>
            <div className={styles['logo-container']}>
                <img src={require('../logo.png')} height="20px" />
                iteTorrent
            </div>
            <button className={styles['button-container']} onClick={add}>
                Add Torrent
            </button>
            <input
                type="file"
                ref={inputRef}
                hidden
                onChange={handleOnChange}
            />
            <button className={styles['button-container']} onClick={remove}>
                Remove Torrent
            </button>
        </div>
    )
}

export default Navbar;