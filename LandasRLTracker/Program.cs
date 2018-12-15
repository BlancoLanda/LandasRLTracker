using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace LandasRLTracker
{
    class Program
    {
        readonly static string RLLogPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\My Games\Rocket League\TAGame\Logs\Launch.log");
        readonly static string LandaLogPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\My Games\Rocket League\TAGame\Logs\LandaRL.log");
        readonly static string streamerKitFolder = @"StreamerKit\";
        public static string steamId;
        public static string steamNickname;
        public static int sessionTotalGames;
        public static int sessionTotalLoses;
        public static int sessionTotalWins;
        public static int sessionTotalMmrRatio;
        public static SortedDictionary<string, List<string>> statsPerPlaylist = new SortedDictionary<string, List<string>>();

        static void Main(string[] args)
        {
            bool init = false;

            Init();

            while(true)
            {
                GetMMR(init);
                System.Threading.Thread.Sleep(10000);
            }
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

            if (File.Exists(LandaLogPath))
            {
                File.Delete(LandaLogPath);
            }

            if (File.Exists(RLLogPath))
            {

                Console.WriteLine("Welcome to Landa's RL Tracker v1.0!");
                Console.WriteLine("----------------------------------------------");
                steamId = SetSteamId();
                steamNickname = SetNicknameFromId(steamId);
                Process[] pname = Process.GetProcessesByName("RocketLeague");
                if (pname.Length == 0)
                {
                    Console.Error.WriteLine("Rocket League process not detected. Are you running Rocket League? Exiting the program...");
                    System.Threading.Thread.Sleep(5000);
                    Environment.Exit(1);
                }
                Console.WriteLine("Steam/RL nickname detected:      {0}\n", steamNickname);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Getting MMR data of your account...\n");
                System.Threading.Thread.Sleep(1000);

                bool init = true;

                GetMMR(init);
                AppendStatsToFiles();
                PrintMmrWelcomeScreen();

                System.Threading.Thread.Sleep(1000);

                Console.WriteLine("\nSTARTED LIVE TRACKING. DO NOT CLOSE THIS WINDOW!");
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("...\n");

                // Initial process finish. Now it's time to start listening to new updates...

            } else
            {
                Console.Error.WriteLine("No Rocket League logs found in your PC. Do you have the game installed? Exiting the program...");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(1);
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
            var steamUserApi = new XmlDocument();
            steamUserApi.Load(@"https://steamcommunity.com/profiles/" + steamId + "/?xml=1");
            string name = steamUserApi.SelectSingleNode("profile/steamID").FirstChild.Value;

            return name;
        }

        static void GetMMR(bool init)
        {
            bool blockEnd = false;

            if (File.Exists(RLLogPath) == true)
            {
                // I copy the Rocket League log and work with it in another location in every instance, because Rocket League process is already accesing and writing this file constantly. I don't want to interrupt it.
                File.Copy(RLLogPath, LandaLogPath);
                // I check the log files upside down (reversely) because MMR data is closer to the end of the file.
                foreach (string line in File.ReadAllLines(LandaLogPath).Reverse())
                {

                    if (blockEnd == false && line.Contains(steamId) && line.Contains("OnSkillSynced"))
                    {
                        // Reached end (upside down start) of skill rating information for this steamId.
                        blockEnd = true;
                        continue;
                    }

                    if (blockEnd == true)
                    {
                        //Start (upside down end) of skill rating information for this steamId.
                        //Process lines here and get information.

                        if (line.Contains("HandleSkillRequestCompleteRPC") || !line.Contains("SkillDecay:"))
                        {
                            File.Delete(LandaLogPath);
                            break;
                        }

                        string playlist = "0";
                        string mmr = "0";
                        string tier = "0";
                        string division = "9";
                        string matchesPlayed = "0";

                        Regex regex = new Regex(@"Playlist[^\w]\d{1,2}[\ ]");
                        Match match = regex.Match(line);
                        if (match.Success)
                        {
                            String value = match.Value;
                            playlist = value.Replace("Playlist=", "");
                        }

                        // Unranked does not use MMR. Exclude it.
                        if (int.Parse(playlist) != 0)
                        {
                            regex = new Regex(@"MMR[^\w][1-9][0-9]*[\.][0-9]{6}");
                            match = regex.Match(line);
                            if (match.Success)
                            {
                                String value = match.Value;
                                mmr = value.Replace("MMR=", "");
                            }
                            if (line.Contains("MatchesPlayed="))
                            {
                                // If line contains MMR but not matches played, neither division/tier. It means the player didn't play this playlist in the current season (O games).
                                regex = new Regex(@"MatchesPlayed[^\w][1-9][0-9]*[\,]");
                                match = regex.Match(line);
                                if (match.Success)
                                {
                                    String value = match.Value;
                                    matchesPlayed = new String(value.Where(Char.IsDigit).ToArray());
                                }
                            } else
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
                                else
                                {
                                    division = "0";
                                }

                            } else
                            {
                                tier = "0";
                                division = "9";
                            }

                            if (!init)
                            {
                                // If this method wasn't called from Init(), it means we have to check if there are updates, and, if it's the case, recalculate de MMR ratio using previous data.

                                if(!statsPerPlaylist.ContainsKey(playlist))
                                {
                                    // If first time playing this playlist in this season, add it to the dictionary.

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
                                    AnnounceNewPlaylist(playlist, mmrInt);
                                    AppendStatsToFiles(playlist);

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
                                        }
                                        else
                                        {
                                            // Match finished with a lose.
                                            playlistSessionLoses = (int.Parse(statsPerPlaylist[playlist][6]) + 1).ToString();
                                            sessionTotalLoses++;
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
                            } else
                            {

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
                            }
                        }
                    }
                }
            }
        }

        // Method that creates files with all the stats. It allow streamers to have live updates of their RL stats on screen.

        static void AppendStatsToFiles()
        {
            foreach (var playlist in statsPerPlaylist.Keys)
            {
                Directory.CreateDirectory(streamerKitFolder);
                Directory.CreateDirectory(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)));

                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr.txt", CalculateRescaledMmr(decimal.Parse((statsPerPlaylist[playlist])[0], CultureInfo.InvariantCulture)).ToString());
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\rank_name.txt", MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])));
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr_session_ratio.txt", int.Parse((statsPerPlaylist[playlist])[4]).ToString("+0;-#"));
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\wins.txt", (statsPerPlaylist[playlist])[5].ToString());
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\loses.txt", (statsPerPlaylist[playlist])[6].ToString());
                string totalPlaylistGames = (int.Parse((statsPerPlaylist[playlist])[5]) + int.Parse((statsPerPlaylist[playlist])[6])).ToString();
                File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\total_games.txt", totalPlaylistGames);

            }

            Directory.CreateDirectory(streamerKitFolder + @"\Global");
            File.WriteAllText(streamerKitFolder + @"\Global\mmr_session_ratio.txt", sessionTotalMmrRatio.ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\Global\wins.txt", sessionTotalWins.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\loses.txt", sessionTotalLoses.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\total_games.txt", sessionTotalGames.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\last_update_timestamp.txt", DateTime.Now.ToString());

        }

        static void AppendStatsToFiles(string playlist)
        {
            Directory.CreateDirectory(streamerKitFolder);
            Directory.CreateDirectory(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)));
            Directory.CreateDirectory(streamerKitFolder + @"\Global");

            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr.txt", CalculateRescaledMmr(decimal.Parse((statsPerPlaylist[playlist])[0], CultureInfo.InvariantCulture)).ToString());
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\rank_name.txt", MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])));
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\mmr_session_ratio.txt", int.Parse((statsPerPlaylist[playlist])[4]).ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\wins.txt", (statsPerPlaylist[playlist])[5].ToString());
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\loses.txt", (statsPerPlaylist[playlist])[6].ToString());
            string totalPlaylistGames = (int.Parse((statsPerPlaylist[playlist])[5]) + int.Parse((statsPerPlaylist[playlist])[6])).ToString();
            File.WriteAllText(streamerKitFolder + @"\" + MapPlaylistName(int.Parse(playlist)) + @"\total_games.txt", totalPlaylistGames);

            File.WriteAllText(streamerKitFolder + @"\Global\mmr_session_ratio.txt", sessionTotalMmrRatio.ToString("+0;-#"));
            File.WriteAllText(streamerKitFolder + @"\Global\wins.txt", sessionTotalWins.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\loses.txt", sessionTotalLoses.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\total_games.txt", sessionTotalGames.ToString());
            File.WriteAllText(streamerKitFolder + @"\Global\last_update_timestamp.txt", DateTime.Now.ToString());
        }

        static void AnnounceUpdate(string playlist, List<string> stats, int mmrWonOrLost, string tierChange, string divisionChange)
        {

            Console.WriteLine("[{0}] New UPDATE of your MMR stats detected!\n", DateTime.Now);

            if (mmrWonOrLost > 0)
                Console.WriteLine("[{0}] You've just won {1} MMR!", MapPlaylistName(int.Parse(playlist)), mmrWonOrLost.ToString("+0;-#"));
            else
                Console.WriteLine("[{0}] You've just lost {1} MMR!", MapPlaylistName(int.Parse(playlist)), mmrWonOrLost);
            if (tierChange.Equals("up"))
            {
                Console.WriteLine("[{0}] You've just ranked up!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (tierChange.Equals("down"))
            {
                Console.WriteLine("[{0}] You've just ranked down!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (divisionChange.Equals("up"))
            {
                Console.WriteLine("[{0}] You've just got one division up!", MapPlaylistName(int.Parse(playlist)));
            }
            else if (divisionChange.Equals("down"))
            {
                Console.WriteLine("[{0}] You've just got one division down!", MapPlaylistName(int.Parse(playlist)));
            }
            Console.WriteLine("[{0}] Current rank: {1} {2}", MapPlaylistName(int.Parse(playlist)), MapTierName(int.Parse(stats[1])), MapDivisionName(int.Parse(stats[2])));
            Console.WriteLine("[{0}] Current MMR: {1}", MapPlaylistName(int.Parse(playlist)), CalculateRescaledMmr(decimal.Parse(stats[0], CultureInfo.InvariantCulture)).ToString());
            Console.WriteLine("[{0}] Playlist Session MMR ratio: {1} ", MapPlaylistName(int.Parse(playlist)), int.Parse(stats[4]).ToString("+0;-#"));
            string playlistTotalGames = (int.Parse(stats[5]) + int.Parse(stats[6])).ToString();
            Console.WriteLine("[{0}] Playlist Session W/L ratio: {1} Games ({2}W, {3}L)\n", MapPlaylistName(int.Parse(playlist)), playlistTotalGames, stats[5], stats[6]);

            Console.WriteLine("[TOTAL] Total Session MMR ratio: {1} ", MapPlaylistName(int.Parse(playlist)), sessionTotalMmrRatio.ToString("+0;-#"));
            Console.WriteLine("[TOTAL] Total Session W/L ratio: {1} Games ({2}W, {3}L)\n", MapPlaylistName(int.Parse(playlist)), sessionTotalGames, sessionTotalWins, sessionTotalLoses);

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("RESUMING THE LIVE TRACKING. DO NOT CLOSE THIS WINDOW!");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("...\n");
        }

        static void AnnounceNewPlaylist(string playlist, int mmr)
        {
            Console.WriteLine("[{0}] New UPDATE of your MMR stats detected!\n", DateTime.Now);

            Console.WriteLine("[{0}] You've just finished your first ranked match in this playlist this season!", MapPlaylistName(int.Parse(playlist)));
            Console.WriteLine("[{0}] Current MMR: {1}", MapPlaylistName(int.Parse(playlist)), mmr.ToString());
            Console.WriteLine("[INFO] Can't determine with the logs if the outcome of 'first playlist matches' are win or lose. Ignoring this match for MMR ratio & W/L ratio calculations.");
            Console.WriteLine("[INFO] Next matches in this playlist won't have this problem! Keep playing :)\n");

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("RESUMING THE LIVE TRACKING. DO NOT CLOSE THIS WINDOW!");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("...\n");
        }
    

        static void PrintMmrWelcomeScreen()
        {

            Console.WriteLine("ACTIVE PLAYLISTS:\n");
            System.Threading.Thread.Sleep(200);

            foreach (var playlist in statsPerPlaylist.Keys)
            {
                // Order of stats in list for each playlist: 1. mmr , 2. tier, 3. division, 4. matchesPlayed

                Console.WriteLine(MapPlaylistName(int.Parse(playlist)) + "  MMR: " + CalculateRescaledMmr(decimal.Parse((statsPerPlaylist[playlist])[0], CultureInfo.InvariantCulture)) + " ( " + MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])) + " )  Matches played: " + (statsPerPlaylist[playlist])[3]);
                System.Threading.Thread.Sleep(100);
            }
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
