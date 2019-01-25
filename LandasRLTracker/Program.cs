using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using MarkdownLog;
using Newtonsoft.Json.Linq;

namespace LandasRLTracker
{
    class Program
    {
        public static string RLLogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Rocket League\TAGame\Logs\Launch.log";
        readonly static string streamerKitFolder = @"StreamerKit\";
        readonly static string version = "v1.5.1";
        public static string steamId;
        public static string steamNickname;
        public static string storedLine;
        public static int sessionTotalGames;
        public static int sessionTotalLoses;
        public static int sessionTotalWins;
        public static int sessionTotalMmrRatio;
        public static int sessionCurrentStreak;
        public static int sessionLongestWStreak;
        public static int sessionLongestLStreak;
        public static Stopwatch stopwatch;
        public static SortedDictionary<string, List<string>> statsPerPlaylist = new SortedDictionary<string, List<string>>();
        public static SortedDictionary<string, List<string>> initialStatsPerPlaylist = new SortedDictionary<string, List<string>>();
        public static List<string> initialPlaylists = new List<string>();


        static void Main(string[] args)
        {
            Init();
            StartLiveTracking();
        }

        static void Init()
        {
            // First things to do:
            // 1. Check if Steam and Rocket League are running.
            // 2. Get Steam ID and nickname from logged in Steam account.
            // 3. Get MMR starting data from local logs.

            sessionTotalLoses = 0;
            sessionTotalWins = 0;
            sessionTotalMmrRatio = 0;
            sessionTotalGames = 0;
            sessionCurrentStreak = 0;
            sessionLongestWStreak = 0;
            sessionLongestLStreak = 0;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            if (File.Exists(RLLogPath))
            {

                Console.WriteLine("Welcome to Landa's RL Tracker " + version);
                Console.WriteLine("----------------------------------------------\n");
                CheckUpdates();
                System.Threading.Thread.Sleep(100);
                PrintInfoTag();
                Console.WriteLine(" Restarting or closing RL won't affect session tracking, but NEVER close this window until you're done!\n");
                PrintInfoTag();
                Console.WriteLine(" Keys (Console window must be active):");
                PrintInfoTag();
                Console.WriteLine(" 'R' -> Reset your stats (including files), like starting a new session.");
                PrintInfoTag();
                Console.WriteLine(" 'S' -> Get a full summary of your current session (Games played, MMR won, ranks gained, etc.).\n");
                System.Threading.Thread.Sleep(1000);
                Process[] RLProcess = Process.GetProcessesByName("RocketLeague");
                if (RLProcess.Length == 0)
                {
                    Console.WriteLine("Rocket League process is not opened. Waiting for it to be opened to start getting data...");
                    while (RLProcess.Length == 0)
                    {
                        // Do nothing, just wait...
                        RLProcess = Process.GetProcessesByName("RocketLeague");
                    }
                    Console.WriteLine("Rocket League process is running now!\n");
                    System.Threading.Thread.Sleep(1000);
                }

                steamId = SetSteamId();
                steamNickname = SetNicknameFromId(steamId);
                Console.Write("\nSteam/RL nickname detected:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" {0}\n", steamNickname);
                Console.ResetColor();

                Console.Write("Getting MMR data of your account...");
                System.Threading.Thread.Sleep(1000);

                GetInitialPlaylists();
                UpdatePlaylistStats();
                AppendStatsToFiles();
                PrintMmrWelcomeScreen();

                System.Threading.Thread.Sleep(1000);

                Console.WriteLine("\nStarted live tracking. Do not close this window!");
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("...");

                // Initial process finish. Now it's time to start listening to new updates...

            } else
            {
                PrintErrorTag();
                Console.Error.WriteLine(" No Rocket League logs found in your PC. Do you have the game installed? Exiting the program...");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(1);
            }

        }

        public static void GetInitialPlaylists()
        {

            bool detectedMmrLogStart = false;
            bool detectedMmrLogEnd = false;

            if (File.Exists(RLLogPath))
            {

                while (!detectedMmrLogStart || !detectedMmrLogEnd)
                {
                    using (var fs = new FileStream(RLLogPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string line = string.Empty;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains("OnlineGameSkill_X::OnlineGameSkill_TA:HandleSkillRequestCompleteRPC"))
                                {
                                    detectedMmrLogStart = true;
                                }
                                if (line.Contains("OnlineGameSkill_X::OnlineGameSkill_TA:OnSkillSynced"))
                                {
                                    detectedMmrLogEnd = true;
                                }

                            }

                            if (!detectedMmrLogStart || !detectedMmrLogEnd)
                            {
                                // Still not MMR data in logs, retrying in 5 seconds.
                                System.Threading.Thread.Sleep(5000);
                            }
                        }
                    }

                }

            }
            else
            {
                PrintErrorTag();
                Console.Error.WriteLine(" No Rocket League logs found in your PC. Do you have the game installed? Exiting the program...");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(1);
            }

            using (var fs = new FileStream(RLLogPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("OnlineGameSkill_X::OnlineGameSkill_TA:OnSkillSynced PlayerID=Steam|" + steamId))
                        {
                            break;
                        }

                        // Logs are not printed line by line. It's printed by blocks of bytes. It can happen that an original line is printed in two different lines. In that case, unify both lines.

                        Regex regex = new Regex(@"^\[[0-9]*[\.][0-9][0-9]\]");
                        Match match = regex.Match(line);
                        if (!match.Success)
                        {
                            line = storedLine + line;
                        }

                        storedLine = line;

                        if (line.Contains("OnlineGameSkill_X::") && line.Contains("Playlist=") && line.Contains("Uid=" + steamId) && line.Contains("MMR=") && line.Contains("Mu="))
                        {
                            // This line contains MMR info about a playlist
                            string playlist = "0";

                            regex = new Regex(@"Playlist[^\w]\d{1,2}[\ ]");
                            match = regex.Match(line);
                            if (match.Success)
                            {
                                String value = match.Value;
                                playlist = value.Replace("Playlist=", "");
                            }

                            if (int.Parse(playlist) != 0)
                            {
                                initialPlaylists.Add(playlist);
                            }
                        }

                    }
                }
            }
        }

        public static void UpdatePlaylistStats()
        {
            for (int i = 0; i < initialPlaylists.Count; i++)
            {
                int playlistOcurrences = 0;
                int currentLine = 0;
                using (var fs = new FileStream(RLLogPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {

                            Regex regex = new Regex(@"^\[[0-9]*[\.][0-9][0-9]\]");
                            Match match = regex.Match(line);
                            if (!match.Success)
                            {
                                line = storedLine + line;
                            }

                            storedLine = line;

                            if (line.Contains("OnlineGameSkill_X::") && line.Contains("Playlist=" + initialPlaylists[i]) && line.Contains("Uid=" + steamId) && line.Contains("MMR=") && line.Contains("Mu="))
                            {
                                playlistOcurrences++;
                            }

                        }

                    }
                }
                using (var fs = new FileStream(RLLogPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = string.Empty;
                        while ((line = sr.ReadLine()) != null)
                        {

                            Regex regex = new Regex(@"^\[[0-9]*[\.][0-9][0-9]\]");
                            Match match = regex.Match(line);
                            if (!match.Success)
                            {
                                line = storedLine + line;
                            }

                            storedLine = line;

                            if (line.Contains("OnlineGameSkill_X::") && line.Contains("Playlist=" + initialPlaylists[i]) && line.Contains("Uid=" + steamId) && line.Contains("MMR=") && line.Contains("Mu="))
                            {

                                currentLine++;

                                if (currentLine < playlistOcurrences)
                                {
                                    continue;
                                }

                                string playlist = initialPlaylists[i];
                                string mmr = "0";
                                string tier = "0";
                                string division = "9";
                                string matchesPlayed = "0";

                                regex = new Regex(@"MMR[^\w][\-]?[0-9][0-9]*[\.][0-9]{6}");
                                match = regex.Match(line);
                                if (match.Success)
                                {
                                    String value = match.Value;
                                    mmr = value.Replace("MMR=", "");
                                }
                                if (line.Contains("MatchesPlayed="))
                                {
                                    // If line contains MMR but not matches played, neither division/tier. It means the player didn't play this playlist in the current season (O games).
                                    regex = new Regex(@"MatchesPlayed[^\w][0-9][0-9]*[\,]");
                                    match = regex.Match(line);
                                    if (match.Success)
                                    {
                                        String value = match.Value;
                                        matchesPlayed = new String(value.Where(Char.IsDigit).ToArray());
                                    }
                                }
                                else
                                {
                                    matchesPlayed = "0";
                                }
                                if (line.Contains("Tier="))
                                {
                                    regex = new Regex(@"Tier[^\w]\d{1,2}[\,]");
                                    match = regex.Match(line);
                                    if (match.Success)
                                    {
                                        String value = match.Value;
                                        tier = new String(value.Where(Char.IsDigit).ToArray());
                                    }

                                    if (line.Contains("Division="))
                                    {
                                        division = line.Substring(line.LastIndexOf("Division=") + 9, 1);
                                    }
                                    else if(line.Contains("Tier=19"))
                                    {
                                        // Avoid showing "Div I" for Grand Champs.
                                        tier = "19";
                                        division = "9";
                                    } else
                                    {
                                        division = "0";
                                    }

                                }
                                else
                                {
                                    tier = "0";
                                    division = "9";
                                }

                                string mmrRatio = "0";
                                string playlistSessionWins = "0";
                                string playlistSessionLoses = "0";

                                List<string> stats = new List<string>
                                {
                                    mmr,
                                    tier,
                                    division,
                                    matchesPlayed,
                                    mmrRatio,
                                    playlistSessionWins,
                                    playlistSessionLoses
                                };

                                if (!statsPerPlaylist.ContainsKey(playlist))
                                {
                                    statsPerPlaylist.Add(playlist, stats);
                                    initialStatsPerPlaylist.Add(playlist, stats);
                                }

                            }

                        }

                    }
                }
            }
  
        }

        static void StartLiveTracking()
        {

            var initialFileSize = new FileInfo(RLLogPath).Length;
            var lastReadLength = initialFileSize - 1024;
            if (lastReadLength < 0) lastReadLength = 0;

            ConsoleKey key;

            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    try
                    {
                        var fileSize = new FileInfo(RLLogPath).Length;
                        if (fileSize > lastReadLength)
                        {
                            using (var fs = new FileStream(RLLogPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                fs.Seek(lastReadLength, SeekOrigin.Begin);
                                var buffer = new byte[1024];

                                while (true)
                                {

                                    var bytesRead = fs.Read(buffer, 0, buffer.Length);
                                    lastReadLength += bytesRead;

                                    if (bytesRead == 0)
                                        break;

                                    var text = ASCIIEncoding.ASCII.GetString(buffer, 0, bytesRead);

                                    using (StringReader sr = new StringReader(text))
                                    {
                                        string line;
                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            Regex regex = new Regex(@"^\[[0-9]*[\.][0-9][0-9]\]");
                                            Match match = regex.Match(line);
                                            if (!match.Success)
                                            {
                                                line = storedLine + line;
                                            }

                                            storedLine = line;

                                            if (line.Contains("OnlineGameSkill_X::") && line.Contains("Playlist=") && line.Contains("Uid=" + steamId) && line.Contains("MMR=") && line.Contains("Mu="))
                                            {
                                                // This line contains MMR info about a playlist
                                                string playlist = "0";
                                                string mmr = "0";
                                                string tier = "0";
                                                string division = "9";
                                                string matchesPlayed = "0";

                                                regex = new Regex(@"Playlist[^\w]\d{1,2}[\ ]");
                                                match = regex.Match(line);
                                                if (match.Success)
                                                {
                                                    String value = match.Value;
                                                    playlist = value.Replace("Playlist=", "");
                                                }

                                                if (int.Parse(playlist) != 0)
                                                {
                                                    regex = new Regex(@"MMR[^\w][\-]?[0-9][0-9]*[\.][0-9]{6}");
                                                    match = regex.Match(line);
                                                    if (match.Success)
                                                    {
                                                        String value = match.Value;
                                                        mmr = value.Replace("MMR=", "");
                                                    }
                                                    if (line.Contains("MatchesPlayed="))
                                                    {
                                                        // If line contains MMR but not matches played, neither division/tier. It means the player didn't play this playlist in the current season (O games).
                                                        regex = new Regex(@"MatchesPlayed[^\w][0-9][0-9]*[\,]");
                                                        match = regex.Match(line);
                                                        if (match.Success)
                                                        {
                                                            String value = match.Value;
                                                            matchesPlayed = new String(value.Where(Char.IsDigit).ToArray());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        matchesPlayed = "0";
                                                    }
                                                    if (line.Contains("Tier="))
                                                    {
                                                        regex = new Regex(@"Tier[^\w]\d{1,2}[\,]");
                                                        match = regex.Match(line);
                                                        if (match.Success)
                                                        {
                                                            String value = match.Value;
                                                            tier = new String(value.Where(Char.IsDigit).ToArray());
                                                        }

                                                        if (line.Contains("Division="))
                                                        {
                                                            division = line.Substring(line.LastIndexOf("Division=") + 9, 1);
                                                        }
                                                        else if (line.Contains("Tier=19"))
                                                        {
                                                            tier = "19";
                                                            division = "9";
                                                        }
                                                        else
                                                        {
                                                            division = "0";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        tier = "0";
                                                        division = "9";
                                                    }
                                                }

                                                if (!statsPerPlaylist.ContainsKey(playlist))
                                                {
                                                    // If first time playing this playlist in this season, add it to the dictionary.

                                                    if (int.TryParse(playlist, out int n))
                                                    {
                                                        int mmrInt = CalculateRescaledMmr(decimal.Parse(mmr, CultureInfo.InvariantCulture));
                                                        string mmrRatio = "0";
                                                        string playlistSessionWins = "0";
                                                        string playlistSessionLoses = "0";

                                                        List<string> stats = new List<string>
                                                    {
                                                        mmr,
                                                        tier,
                                                        division,
                                                        matchesPlayed,
                                                        mmrRatio,
                                                        playlistSessionWins,
                                                        playlistSessionLoses
                                                    };

                                                        statsPerPlaylist.Add(playlist, stats);
                                                        initialStatsPerPlaylist.Add(playlist, stats);
                                                        AnnounceNewPlaylist(playlist, mmrInt);
                                                        AppendStatsToFiles(playlist);
                                                    }

                                                }
                                                else
                                                {
                                                    int numericMmr = CalculateRescaledMmr(decimal.Parse(mmr, CultureInfo.InvariantCulture));
                                                    int numericPreviousMmr = CalculateRescaledMmr(decimal.Parse((statsPerPlaylist[playlist])[0], CultureInfo.InvariantCulture));

                                                    if (numericMmr != numericPreviousMmr)
                                                    {

                                                        // Changes in MMR in this playlist
                                                        int mmrWonOrLost = numericMmr - numericPreviousMmr;
                                                        string playlistSessionWins = statsPerPlaylist[playlist][5];
                                                        string playlistSessionLoses = statsPerPlaylist[playlist][6];
                                                        string mmrRatio = (int.Parse(statsPerPlaylist[playlist][4]) + mmrWonOrLost).ToString();
                                                        sessionTotalMmrRatio = sessionTotalMmrRatio + mmrWonOrLost;
                                                        sessionTotalGames++;
                                                        if (mmrWonOrLost > 0)
                                                        {
                                                            // Match finished with a win.
                                                            playlistSessionWins = (int.Parse(statsPerPlaylist[playlist][5]) + 1).ToString();
                                                            sessionTotalWins++;

                                                            if(sessionCurrentStreak < 0)
                                                            {
                                                                // End of current losing streak.
                                                                sessionCurrentStreak = 1;
                                                            } else
                                                            {
                                                                // Current winning streak keeps going.
                                                                sessionCurrentStreak++;
                                                            }

                                                            if(sessionCurrentStreak > sessionLongestWStreak)
                                                            {
                                                                sessionLongestWStreak = sessionCurrentStreak;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            // Match finished with a lose.
                                                            playlistSessionLoses = (int.Parse(statsPerPlaylist[playlist][6]) + 1).ToString();
                                                            sessionTotalLoses++;

                                                            if (sessionCurrentStreak > 0)
                                                            {
                                                                // End of current winning streak.
                                                                sessionCurrentStreak = -1;
                                                            } else
                                                            {
                                                                // Current losing streak keeps going.
                                                                sessionCurrentStreak--;
                                                            }

                                                            if (sessionCurrentStreak < sessionLongestLStreak)
                                                            {
                                                                sessionLongestLStreak = sessionCurrentStreak;
                                                            }

                                                        }

                                                        string tierChange = "no";
                                                        string divisionChange = "no";
                                                        int numericTier = int.Parse(tier);
                                                        int numericPreviousTier = int.Parse(statsPerPlaylist[playlist][1]);
                                                        int numericDivision = int.Parse(division);
                                                        int numericPreviousDivision = int.Parse(statsPerPlaylist[playlist][2]);

                                                        if (numericTier != numericPreviousTier)
                                                        {
                                                            if (numericTier > numericPreviousTier)
                                                            {
                                                                // Rank up
                                                                tierChange = "up";
                                                            }
                                                            else
                                                            {
                                                                // Rank down
                                                                tierChange = "down";
                                                            }
                                                        }
                                                        else if (numericDivision != numericPreviousDivision)
                                                        {
                                                            if (numericDivision > numericPreviousDivision)
                                                            {
                                                                // Div up
                                                                divisionChange = "up";
                                                            }
                                                            else
                                                            {
                                                                // Div down
                                                                divisionChange = "down";
                                                            }
                                                        }

                                                        List<string> stats = new List<string>
                                                    {
                                                        mmr,
                                                        tier,
                                                        division,
                                                        matchesPlayed,
                                                        mmrRatio,
                                                        playlistSessionWins,
                                                        playlistSessionLoses
                                                    };

                                                        statsPerPlaylist[playlist] = stats;

                                                        // Time to announce the update in console.
                                                        AnnounceUpdate(playlist, stats, mmrWonOrLost, tierChange, divisionChange);
                                                        AppendStatsToFiles(playlist);
                                                    }
                                                }

                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    catch { }

                    System.Threading.Thread.Sleep(1000);
                    Process[] RLProcess = Process.GetProcessesByName("RocketLeague");
                    if (RLProcess.Length == 0)
                    {
                        Console.WriteLine("Rocket League process has been closed. Waiting for it to be opened to keep getting data!");
                        System.Threading.Thread.Sleep(1000);
                        SessionSummary(true);
                        while (RLProcess.Length == 0)
                        {
                            // Do nothing, just wait...
                            RLProcess = Process.GetProcessesByName("RocketLeague");
                        }
                        Console.WriteLine("Rocket League process is back up. Resuming live tracking. Do not close this window!");
                        Console.WriteLine("...");
                        System.Threading.Thread.Sleep(20000);
                        initialFileSize = new FileInfo(RLLogPath).Length;
                        lastReadLength = initialFileSize - 1024;
                        if (lastReadLength < 0) lastReadLength = 0;
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.R)
                {
                    Console.WriteLine("You pressed the R key! All stats are reset to 0. New session starting now!");
                    Console.WriteLine("...");
                    foreach (var playlist in statsPerPlaylist.Keys)
                    {
                        statsPerPlaylist[playlist][4] = "0";
                        statsPerPlaylist[playlist][5] = "0";
                        statsPerPlaylist[playlist][6] = "0";
                    }

                    sessionTotalGames = 0;
                    sessionTotalLoses = 0;
                    sessionTotalWins = 0;
                    sessionTotalMmrRatio = 0;
                    sessionCurrentStreak = 0;
                    sessionLongestLStreak = 0;
                    sessionLongestWStreak = 0;
                    stopwatch.Restart();
                    AppendStatsToFiles();
                    initialStatsPerPlaylist = statsPerPlaylist;

                }

                if (key == ConsoleKey.S)
                {
                    Console.WriteLine("You pressed the S key! You just requested a summary of this session.\n");
                    SessionSummary(false);

                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine("Resuming live tracking. Do not close this window!");
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("...");
                }
            }
        }

        static string SetSteamId()
        {
            string id;
            using (var steam = new SteamBridge())
            {
                id = steam.GetSteamId().ToString(CultureInfo.InvariantCulture);
            }
            return id;
        }

        static string SetNicknameFromId(String steamId)
        {
            string name = "";
            var steamUserApi = new XmlDocument();
            steamUserApi.Load(@"https://steamcommunity.com/profiles/" + steamId + "/?xml=1");
            try
            {
                name = steamUserApi.SelectSingleNode("profile/steamID").FirstChild.Value;
            }
            catch (NullReferenceException)
            {
                PrintErrorTag();
                Console.Error.WriteLine(" Steam is opened but not logged in. Log in, open Rocket League and try again! Exiting program...");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(1);
            }

            return name;
        }

        static string GetLatestVersion()
        {

            const string GITHUB_API = "https://api.github.com/repos/BlancoLanda/LandasRLTracker/releases";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Landas RL Tracker");
            Uri uri = new Uri(GITHUB_API);
            string releases = webClient.DownloadString(uri);
            JArray a = JArray.Parse(releases);
            string tagName = "";
            foreach (JObject content in a.Children<JObject>())
            {
                foreach (JProperty prop in content.Properties())
                {
                    string name = prop.Name.ToString();
                    if(name.Equals("tag_name"))
                    {
                       tagName = prop.Value.ToString();
                       return tagName;
                    }
                }
            }
            return tagName;
        }

        static void CheckUpdates()
        {
            
            if(!GetLatestVersion().Equals(version))
            {
                PrintInfoTag();
                Console.WriteLine(" Version outdated! This version: {0}, New version: {1}", version, GetLatestVersion());
                PrintInfoTag();
                Console.WriteLine(@" Download latest version at https://github.com/BlancoLanda/LandasRLTracker for better functionality!");
            }
            
        }

        // Method that creates files with all the stats. It allow streamers to have live updates of their RL stats on screen.

        static void AppendStatsToFiles()
        {
            foreach (var playlist in statsPerPlaylist.Keys)
            {
                Directory.CreateDirectory(streamerKitFolder);
                Directory.CreateDirectory(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)));

                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr.txt", CalculateRescaledMmr(decimal.Parse(statsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString());
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\rank_name.txt", MapTierName(int.Parse(statsPerPlaylist[playlist][1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])));
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr_session_ratio.txt", int.Parse(statsPerPlaylist[playlist][4]).ToString("+0;-#"));
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\wins.txt", statsPerPlaylist[playlist][5].ToString());
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\loses.txt", statsPerPlaylist[playlist][6].ToString());
                string totalPlaylistGames = (int.Parse((statsPerPlaylist[playlist])[5]) + int.Parse((statsPerPlaylist[playlist])[6])).ToString();
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\total_games.txt", totalPlaylistGames);
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\win_rate.txt", GetWinPercentage(int.Parse(statsPerPlaylist[playlist][5]), int.Parse(statsPerPlaylist[playlist][6])).ToString("0.##") + "%");

            }

            Directory.CreateDirectory(streamerKitFolder + @"\Global");
            File.WriteAllText(streamerKitFolder + @"\Global\mmr_session_ratio.txt", sessionTotalMmrRatio.ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\Global\wins.txt", sessionTotalWins.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\loses.txt", sessionTotalLoses.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\total_games.txt", sessionTotalGames.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\last_update_timestamp.txt", DateTime.Now.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\current_streak.txt", GetStreakStringByInt(sessionCurrentStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\longest_winning_streak.txt", GetStreakStringByInt(sessionLongestWStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\longest_losing_streak.txt", GetStreakStringByInt(sessionLongestLStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\global_win_rate.txt", GetWinPercentage(sessionTotalWins, sessionTotalLoses).ToString("0.##") + "%");

        }

        static void AppendStatsToFiles(string playlist)
        {
            Directory.CreateDirectory(streamerKitFolder);
            Directory.CreateDirectory(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)));
            Directory.CreateDirectory(streamerKitFolder + @"\Global");

            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr.txt", CalculateRescaledMmr(decimal.Parse(statsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString());
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\rank_name.txt", MapTierName(int.Parse(statsPerPlaylist[playlist][1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])));
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr_session_ratio.txt", int.Parse(statsPerPlaylist[playlist][4]).ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\wins.txt", statsPerPlaylist[playlist][5].ToString());
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\loses.txt", statsPerPlaylist[playlist][6].ToString());
            string totalPlaylistGames = (int.Parse((statsPerPlaylist[playlist])[5]) + int.Parse(statsPerPlaylist[playlist][6])).ToString();
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\total_games.txt", totalPlaylistGames);
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\win_rate.txt", GetWinPercentage(int.Parse(statsPerPlaylist[playlist][5]), int.Parse(statsPerPlaylist[playlist][6])).ToString("0.##") + "%");

            File.WriteAllText(streamerKitFolder + @"\Global\mmr_session_ratio.txt", sessionTotalMmrRatio.ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\Global\wins.txt", sessionTotalWins.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\loses.txt", sessionTotalLoses.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\total_games.txt", sessionTotalGames.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\last_update_timestamp.txt", DateTime.Now.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\current_streak.txt", GetStreakStringByInt(sessionCurrentStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\longest_winning_streak.txt", GetStreakStringByInt(sessionLongestWStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\longest_losing_streak.txt", GetStreakStringByInt(sessionLongestLStreak));
            File.WriteAllText(streamerKitFolder + @"\Global\global_win_rate.txt", GetWinPercentage(sessionTotalWins, sessionTotalLoses).ToString("0.##") + "%");
        }

        static void AnnounceUpdate(string playlist, List<string> stats, int mmrWonOrLost, string tierChange, string divisionChange)
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}]", DateTime.Now);
            Console.ResetColor();
            Console.Write(" New UPDATE of your MMR stats detected! Playlist: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(MapPlaylistName(int.Parse(playlist)));
            Console.ResetColor();
            Console.WriteLine("\n");

            if (mmrWonOrLost > 0)
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just won {1} MMR!", MapPlaylistName(int.Parse(playlist)), mmrWonOrLost.ToString("+0;-#"));
            }

            else
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just lost {1} MMR!", MapPlaylistName(int.Parse(playlist)), mmrWonOrLost);
            }

            if (tierChange.Equals("up"))
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just ranked up!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (tierChange.Equals("down"))
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just ranked down!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (divisionChange.Equals("up"))
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just got one division up!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (divisionChange.Equals("down"))
            {
                PrintPlaylistTag(playlist);
                Console.WriteLine(" You've just got one division down!", MapPlaylistName(int.Parse(playlist)));
            }
            PrintPlaylistTag(playlist);
            Console.Write(" Current rank: {0} {1}\n", MapTierName(int.Parse(stats[1])), MapDivisionName(int.Parse(stats[2])));
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Current MMR: {1}", MapPlaylistName(int.Parse(playlist)), CalculateRescaledMmr(decimal.Parse(stats[0], CultureInfo.InvariantCulture)).ToString());
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Playlist Session MMR ratio: {1} ", MapPlaylistName(int.Parse(playlist)), int.Parse(stats[4]).ToString("+0;-#"));
            string playlistTotalGames = (int.Parse(stats[5]) + int.Parse(stats[6])).ToString();
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Playlist Session W/L ratio: {1} Games ({2}W, {3}L) [Win Rate: {4}%]\n", MapPlaylistName(int.Parse(playlist)), playlistTotalGames, stats[5], stats[6], GetWinPercentage(int.Parse(stats[5]), int.Parse(stats[6])).ToString("0.##"));

            PrintGlobalTag();
            Console.WriteLine(" Total Session MMR ratio: {1} ", MapPlaylistName(int.Parse(playlist)), sessionTotalMmrRatio.ToString("+0;-#"));
            PrintGlobalTag();
            Console.WriteLine(" Total Session W/L ratio: {1} Games ({2}W, {3}L) [Win Rate: {4}%]\n", MapPlaylistName(int.Parse(playlist)), sessionTotalGames, sessionTotalWins, sessionTotalLoses, GetWinPercentage(sessionTotalWins, sessionTotalLoses).ToString("0.##"));

            PrintGlobalTag();
            Console.WriteLine(" Session current streak: {0} ", GetStreakStringByInt(sessionCurrentStreak));
            PrintGlobalTag();
            Console.WriteLine(" Session longest winning streak: {0}. Longest losing streak: {1}\n", GetStreakStringByInt(sessionLongestWStreak), GetStreakStringByInt(sessionLongestLStreak));

            PrintGlobalTag();
            Console.WriteLine(" Session total time: {0:hh\\:mm\\:ss}\n", stopwatch.Elapsed);

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Resuming live tracking. Do not close this window!");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("...");
        }

        static void SessionSummary(bool rlClosed)
        {

            var PlaylistSummary = new List<PlaylistSummary>();

            foreach (var playlist in statsPerPlaylist.Keys)
            {
                // Check if final number of matches played equals to initial one. If true, it means the playlist was active in this session. If false, playlist not active, and ignores it.
                if(initialStatsPerPlaylist[playlist][3] == statsPerPlaylist[playlist][3])
                {
                continue;
                } else
                {
                    string name = MapPlaylistName(int.Parse(playlist));
                    int matchesPlayed = int.Parse(statsPerPlaylist[playlist][3]) - int.Parse(initialStatsPerPlaylist[playlist][3]);
                    string winsNumber = statsPerPlaylist[playlist][5];
                    decimal winRate = GetWinPercentage(int.Parse(winsNumber), int.Parse(statsPerPlaylist[playlist][6]));
                    string wins = winsNumber + " (" + winRate.ToString("0.##") + "%)";
                    int loses = int.Parse(statsPerPlaylist[playlist][6]);
                    string mmrRatio = int.Parse(statsPerPlaylist[playlist][4]).ToString("+0;-#");
                    string initialMmr = CalculateRescaledMmr(decimal.Parse(initialStatsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString();
                    string currentMmr = CalculateRescaledMmr(decimal.Parse(statsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString();
                    string initialRankName = MapTierName(int.Parse((initialStatsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((initialStatsPerPlaylist[playlist])[2])) + " (" + initialMmr + " MMR)";
                    string currentRankName = MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])) + " (" + currentMmr + " MMR)";

                    PlaylistSummary.Add(new PlaylistSummary { Name = name, matchesPlayed = matchesPlayed, winsAndWinrate = wins, loses = loses, mmrRatio = mmrRatio, initialRank = initialRankName, currentRank = currentRankName });
                }
            }

            if(!PlaylistSummary.Any())
            {
                if(!rlClosed)
                {
                    Console.WriteLine("You asked for the summary of the current session... but you still didn't play any match! Start playing and try again later.\n");
                }   
            } else
            {

                if(rlClosed)
                {
                    Console.WriteLine("Let me show you the summary of the current session!\n");
                }

                Console.WriteLine("     ACTIVE PLAYLISTS THIS SESSION:\n");
                System.Threading.Thread.Sleep(200);
                Console.Write(PlaylistSummary.ToMarkdownTable().WithHeaders("Playlist", "Matches played", "Wins", "Loses", "MMR Ratio", "Initial rank", "Current rank"));
                Console.WriteLine();
                Console.WriteLine(" Session total time: {0:hh\\:mm\\:ss}\n", stopwatch.Elapsed);

            }

        }

        static decimal GetWinPercentage(int wins, int loses)
        {
            int totalGames = wins + loses;

            if (totalGames == 0)
            {
                return 0;
            } else
            {

                decimal winPercentage = ((decimal)wins / (decimal)totalGames) * 100;
                return winPercentage;

            }

        }

        static string GetStreakStringByInt(int streak)
        {
            string streakString = "";

            if(streak<0)
            {
                streak = Math.Abs(streak);
                streakString = streak.ToString() + "L";
            } else if (streak==0)
            {
                streakString = streak.ToString();
            } else
            {
                streakString = streak.ToString() + "W";
            }

            return streakString;

        }

        static void PrintPlaylistTag(string playlist)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[" + MapPlaylistName(int.Parse(playlist)) + "]");
            Console.ResetColor();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void PrintGlobalTag()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[GLOBAL]");
            Console.ResetColor();
        }

        static void PrintInfoTag()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[INFO]");
            Console.ResetColor();
        }

        public static void PrintErrorTag()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]");
            Console.ResetColor();
        }

        static void AnnounceNewPlaylist(string playlist, int mmr)
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[{0}]", DateTime.Now);
            Console.ResetColor();
            Console.Write(" New UPDATE of your MMR stats detected! Playlist: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(MapPlaylistName(int.Parse(playlist)));
            Console.ResetColor();
            Console.WriteLine("\n");

            PrintPlaylistTag(playlist);
            Console.WriteLine(" You've just finished your first ranked match in this playlist this season!", MapPlaylistName(int.Parse(playlist)));
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Current MMR: {1}", MapPlaylistName(int.Parse(playlist)), mmr.ToString());
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Can't determine with the logs if the outcome of 'first playlist matches of the season' are win or lose. Ignoring this match for MMR ratio & W/L ratio calculations.");
            PrintPlaylistTag(playlist);
            Console.WriteLine(" Next matches in this playlist won't have this problem! Keep playing :)\n");

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Resuming live tracking. Do not close this window!");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("...");
        }

        
        static void PrintMmrWelcomeScreen()
        {

            var Playlists = new List<Playlist>();

            foreach (var playlist in statsPerPlaylist.Keys)
            {
                Playlists.Add(new Playlist { Name = MapPlaylistName(int.Parse(playlist)), MMR = CalculateRescaledMmr(decimal.Parse((statsPerPlaylist[playlist])[0], CultureInfo.InvariantCulture)) , RankName = MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])) , MatchesPlayed = int.Parse((statsPerPlaylist[playlist])[3]) });
            }

            Console.Write(" Done!\n\n");
            Console.WriteLine("     ACTIVE PLAYLISTS THIS SEASON:\n");
            System.Threading.Thread.Sleep(200);
            Console.Write(Playlists.ToMarkdownTable().WithHeaders("Playlist", "MMR", "Rank name", "Matches played"));

        }
        
        public static int CalculateRescaledMmr(decimal mmr)
        {
            int rescaledMmr = Convert.ToInt32(Math.Round(((20*mmr) + 100), 0));
            return rescaledMmr;
        }

        public static string MapPlaylistName(int playlist)
        {
            var map = new Dictionary<int, string>()
            {
                { 10, "Solo Duel (1vs1)" },
                { 11, "Doubles (2vs2)" },
                { 12, "Solo Standard (3vs3)" },
                { 13, "Standard (3vs3)" },
                { 27, "Hoops" },
                { 28, "Rumble" },
                { 29, "Dropshot" },
                { 30, "Snow Day" }
            };

            return map[playlist];

        }

        public static string MapTierName(int tier)
        {
            var map = new Dictionary<int, string>()
            {
                { 0, "Unranked" },
                { 1, "Bronze I" },
                { 2, "Bronze II" },
                { 3, "Bronze III" },
                { 4, "Silver I" },
                { 5, "Silver II" },
                { 6, "Silver III" },
                { 7, "Gold I" },
                { 8, "Gold II" },
                { 9, "Gold III" },
                { 10, "Platinum I" },
                { 11, "Platinum II" },
                { 12, "Platinum III" },
                { 13, "Diamond I" },
                { 14, "Diamond II" },
                { 15, "Diamond III" },
                { 16, "Champion I" },
                { 17, "Champion II" },
                { 18, "Champion III" },
                { 19, "Grand Champion" }
            };

            return map[tier];

        }

        public static string MapDivisionName(int division)
        {
            var map = new Dictionary<int, string>()
            {
                { 0, "Div I" },
                { 1, "Div II" },
                { 2, "Div III" },
                { 3, "Div IV" },
                { 9, "" }
                // 9: Special case for unranked.
            };

            return map[division];

        }

    }

}