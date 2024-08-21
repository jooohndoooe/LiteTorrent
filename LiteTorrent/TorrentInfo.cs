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
        string Name { get; set; }
        double TotalCompletion { get; set; }
        int TotalFiles { get; set; }
        List<string> FileNames { get; set; }
        List<Double> Completion { get; set; }
        string state { get; set; }

        public TorrentInfo()
        {
            Name = "";
            TotalCompletion = 0;
            TotalFiles = 0;
            FileNames = new List<string>();
            Completion = new List<Double>();
            state = "";
        }

        public void Update(TorrentManager manager)
        {
            Name = manager.Name;
            TotalCompletion = manager.Progress;
            TotalFiles = manager.Files.Count;
            foreach (var file in manager.Files)
            {
                FileNames.Add(file.FullPath.Substring(Path.Combine(Environment.CurrentDirectory, "Downloads").Length + Name.Length + 2));
                Completion.Add(file.BitField.PercentComplete);
            }
            state = manager.State.ToString();
        }

        public void ToConsole()
        {
            Console.WriteLine();
            Console.WriteLine(Name + " " + state + " " + Utils.ProgressBar((int)TotalCompletion, 10) + " " + Math.Round(TotalCompletion, 2) + "%");
            for (int i = 0; i < TotalFiles; i++)
            {
                Console.WriteLine((i + 1) + ". " + FileNames[i] + " " + Utils.ProgressBar((int)Completion[i], 10) + " " + Math.Round(Completion[i], 2) + "%");
            }
        }

        public string GetName()
        {
            return Name;
        }
    }
}
