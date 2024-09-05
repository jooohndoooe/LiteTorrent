using MonoTorrent.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LiteTorrent
{
    public class TorrentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalCompletion { get; set; }
        public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        public string state { get; set; }

        public void Update(TorrentManager manager)
        {
            Id = -1;
            Name = manager.Name;
            TotalCompletion = manager.Progress;
            foreach (var file in manager.Files)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.Update(file.FullPath.Substring(Path.Combine(Environment.CurrentDirectory, "Downloads").Length + Name.Length + 2), file.BitField.PercentComplete);
                Files.Add(fileInfo);
            }
            state = manager.State.ToString();
        }

        public void SetId(int id) {
            Id = id;
        }

        public void ToConsole()
        {
            Console.WriteLine();
            Console.WriteLine(Name + " " + state + " " + Utils.ProgressBar((int)TotalCompletion, 10) + " " + Math.Round(TotalCompletion, 2) + "%");
            for (int i = 0; i < Files.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + Files[i].Name + " " + Utils.ProgressBar((int)Files[i].Completion, 10) + " " + Math.Round(Files[i].Completion, 2) + "%");
            }
        }

        public string GetName()
        {
            return Name;
        }
    }
}
