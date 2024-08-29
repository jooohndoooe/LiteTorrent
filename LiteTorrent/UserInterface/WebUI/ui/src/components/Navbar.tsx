import React, {useState} from "react";

function Navbar(){
    return(
        <div>
            <nav className="navbar">
                <a href ="/" className="site-title">LiteTorrent</a>
                <ul>
                    <li>
                        <button>Add Torrent</button>
                    </li>
                    <li>
                        <button>Remove Torrent</button>
                    </li>
                </ul>
            </nav>
        </div>
    )
}

export default Navbar;