import { useState } from "react";
import TorrentClient from "./components/TorrentClient";

function App() {
    const [selectedId, setSelectedId] = useState(-1);

    return (<TorrentClient selectedId={selectedId} setSelectedId={setSelectedId} />);
}

export default App;
