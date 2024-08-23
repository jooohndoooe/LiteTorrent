using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteTorrent.TorrentManagerServices;

namespace LiteTorrent.UserInterface.ConsoleUI
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public bool isRunning = true;
        private TorrentManagerService TorrentManager { get; set; }

        public ConsoleUserInterface(TorrentManagerService torrentManagerService) { 
            this.TorrentManager = torrentManagerService;
        }

        public async Task Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Greeting();
            while (isRunning)
            {
                string input = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("\x1b[3J");

                string[] inputSplit = input.Split(' ');
                string command = inputSplit[0].ToLower();
                string data = "";
                if (inputSplit.Length > 1)
                {
                    data = input.Substring(command.Length + 1);
                }
                switch (command)
                {
                    case "add":
                        try
                        {
                            byte[] torrentBytes = File.ReadAllBytes(data);
                            Console.WriteLine("Adding " + data);
                            await TorrentManager.AddTorrent(torrentBytes);
                            Console.WriteLine("Done");
                        }
                        catch (MonoTorrent.TorrentException e)
                        {
                            Console.WriteLine("Torrent already added");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Please input a valid path");
                        }
                        break;

                    case "remove":
                        try
                        {
                            Console.WriteLine("Removing torrent " + data);
                            await TorrentManager.RemoveTorrent(int.Parse(data) - 1);
                            Console.WriteLine("Done");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Please enter a valid id");
                        }
                        break;

                    case "progress":
                        TorrentListToConsole(await TorrentManager.GetTorrentList());
                        break;

                    case "list":
                        TorrentListNamesToConsole(await TorrentManager.GetTorrentList());
                        break;

                    case "help":
                        Console.WriteLine("List of the availble Commands:");
                        GetCommandList();
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public void Greeting()
        {
            Console.WriteLine("Greetings!");
            Console.WriteLine("Here is a list of all awailable commands:");
            GetCommandList();
        }

        private void TorrentListToConsole(List<TorrentInfo> torrentList)
        {
            foreach (var torrentInfo in torrentList)
            {
                torrentInfo.ToConsole();
            }
        }

        private void TorrentListNamesToConsole(List<TorrentInfo> torrentList)
        {
            for (int i = 0; i < torrentList.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + ": " + torrentList[i].GetName());
            }
        }

        private void GetCommandList()
        {
            Console.WriteLine("add *file* - add torrent file to downloader");
            Console.WriteLine("remove *id of the file to removed* - remove torrent from downloading");
            Console.WriteLine("progress - detailed progress chart of all torrents");
            Console.WriteLine("list - lit of currently active torrents");
            Console.WriteLine("help - help");
            Console.WriteLine("exit - exit the application");
        }
    }
}
