using LiteTorrent.UserInterface.ConsoleUI;
using LiteTorrent.UserInterface;
using LiteTorrent.UserInterface.WebUI;


const int httpListeningPort = 55125;

var services = new ServiceCollection();

services.AddKeyedSingleton<IUserInterface, ConsoleUserInterface>("cli");
services.AddKeyedSingleton<IUserInterface, WebUserInterface>("web");
await using var provider = services.BuildServiceProvider();

var consoleUI = provider.GetRequiredKeyedService<IUserInterface>("web");
await consoleUI.Run();