# LiteTorrent

An ASP.Net application designed for downloading torrent files. This repository contains two versions of the application:
- A web-based version built with ASP.Net and a React frontend;
- A console-based version for users who prefer a command-line interface.

Both versions use the MonoTorrent library to interact with torrent files, enabling reliable and efficient torrent downloading and management.

## Features
- Download Torrent Files: Easily initiate and manage torrent downloads;
- Cross-Platform: Works on Windows, macOS, and Linux (wherever .NET Core is supported);
- Console and Web Interface: Choose between a React-based UI for web access or a simple console application.

## Table of Contents
- Requirements
- Installation
- Usage
  - Website Version
  - Console Version

## Requirements
.NET 6.0 or later

Node.js 14.0 or later (for React front-end)

MonoTorrent library

## Installation
### Clone the repository:

```
git clone https://github.com/jooohndoooe/LiteTorrent.git
cd LiteTorrent
```

### Install dependencies:

For backend:

```
dotnet restore
```

For React frontend:

```
cd lite-torrent
npm install
```

### Build the application:

```
dotnet build
```

## Usage

### Website Version
Start the ASP.Net backend:

```
dotnet run --project LiteTorrent/UserInterface/WebUI
```
Start the React frontend:

```
cd lite-torrent
npm start
```

Access the application in your browser at http://localhost:5000.

### Console Version
Navigate to the console version:

```
cd LiteTorrent/UserInterface/ConsoleUI
```

Run the console app:

```
dotnet run
```

Follow on-screen instructions to download and manage torrents.
