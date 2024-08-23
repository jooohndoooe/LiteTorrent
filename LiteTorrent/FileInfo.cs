namespace LiteTorrent
{
    public class FileInfo
    {
        public string Name { get; set; }
        public double Completion { get; set; }
        public void Update(string name, double completion) { 
            Name = name;
            Completion = completion;
        }
    }
}
