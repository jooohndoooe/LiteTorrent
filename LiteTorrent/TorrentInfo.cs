using MonoTorrent.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LiteTorrent
{
    class TorrentInfo
    {
        string name;
        double totalCompletion;
        int totalFiles;
        List<string> fileNames;
        List<Double> completion;
        string state;

        public TorrentInfo() {
            name = "";
            totalCompletion = 0;
            totalFiles = 0;
            fileNames = new List<string>();
            completion = new List<Double>();
            state = "";
        }

        public TorrentInfo(TorrentManager manager) {
            name = manager.Name;
            totalCompletion = manager.Progress;
            totalFiles = manager.Files.Count;
            fileNames = new List<string>();
            completion = new List<Double>();
            foreach (var file in manager.Files)
            {
                fileNames.Add(file.FullPath.Substring(Path.Combine(Environment.CurrentDirectory, "Downloads").Length + name.Length + 2));
                completion.Add(file.BitField.PercentComplete);
            }
            state = manager.State.ToString();
        }

        public void Update(TorrentManager manager) { 
            name = manager.Name;
            totalCompletion = manager.Progress;
            totalFiles = manager.Files.Count;
            foreach (var file in manager.Files) {
                fileNames.Add(file.FullPath.Substring(Path.Combine(Environment.CurrentDirectory, "Downloads").Length + name.Length + 2));
                completion.Add(file.BitField.PercentComplete);
            }
        }

        public void ToConsole() {
            Console.WriteLine();
            Console.WriteLine(name + " " + state + " " + Utils.ProgressBar((int)totalCompletion,10) + " " + totalCompletion);
            for (int i = 0; i < totalFiles; i++) {
                Console.WriteLine((i + 1) + ". " + fileNames[i] + " " + Utils.ProgressBar((int)completion[i], 10) + " " + completion[i]);
            }
        }
    }
}
