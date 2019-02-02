using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using LandasRLTracker.Properties;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;

namespace LandasRLTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (Settings.Default.minimizeOnLaunchChecked)
            {
                this.WindowState = FormWindowState.Minimized;
            }

        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        public PrivateFontCollection mFontCollection = new PrivateFontCollection();
        public PrivateFontCollection pfc = new PrivateFontCollection();
        public string RLLogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\Rocket League\TAGame\Logs\Launch.log";
        readonly string streamerKitFolder = @"StreamerKit\";
        readonly string version = "v2.0.0";
        public System.Threading.Thread checkRLStateThread;
        public System.Threading.Thread tRLProccess;
        public System.Threading.Thread tLiveTracking;
        public  Process[] RLProcess;
        public bool isRlOn = false;
        public bool logBeingWritten = false;
        public bool trackerThreadNeeded = false;
        public string currentData;
        public string steamId;
        public string steamNickname;
        public string storedLine;
        public int sessionTotalGames;
        public int sessionTotalLoses;
        public int sessionTotalWins;
        public int sessionTotalMmrRatio;
        public int sessionCurrentStreak;
        public int sessionLongestWStreak;
        public int sessionLongestLStreak;
        public List<string> initialPlaylists = new List<string>();
        public SortedDictionary<string, List<string>> statsPerPlaylist = new SortedDictionary<string, List<string>>();
        public SortedDictionary<string, List<string>> initialStatsPerPlaylist = new SortedDictionary<string, List<string>>();
        public BindingList<Timeline> timelineList = new BindingList<Timeline>();

        private void Form1_Load(object sender, EventArgs e)
        {

            notifyIcon.ContextMenuStrip = contextMenuStrip;

            if (this.WindowState == FormWindowState.Minimized && Settings.Default.minimizeToTrayChecked)
            {
                Hide();
                notifyIcon.Visible = true;
            }

            runOnStartupToolStripMenuItem.Checked = Settings.Default.runOnStartupChecked;
            minimizeOnLaunchToolStripMenuItem.Checked = Settings.Default.minimizeOnLaunchChecked;
            minimizeToTrayToolStripMenuItem.Checked = Settings.Default.minimizeToTrayChecked;


            // Create 'StreamerKit' directory (if doesn't yet exist)

            DirectoryInfo di = Directory.CreateDirectory("StreamerKit");

            // Load custom fonts.
            InitCustomLabelFont();

            InitLabelsAndImages();

            // Start process that checks if RL is running or not.

            checkRLStateThread = new System.Threading.Thread(CheckRLState)
            {
                IsBackground = true
            };
            checkRLStateThread.Start();

            tRLProccess = new System.Threading.Thread(LoopCheckRLProccess)
            {
                IsBackground = true
            };
            tRLProccess.Start();

        }

        public void InitCustomLabelFont()
        {

            Stream fontStreamRegular = new MemoryStream(Resources.BebasNeue_Regular);
            Stream fontStreamBold = new MemoryStream(Resources.BebasNeue_Bold);

            IntPtr dataRegular = Marshal.AllocCoTaskMem((int)fontStreamRegular.Length);
            IntPtr dataBold = Marshal.AllocCoTaskMem((int)fontStreamBold.Length);

            Byte[] fontDataRegular = new Byte[fontStreamRegular.Length];
            Byte[] fontDataBold = new Byte[fontStreamBold.Length];

            fontStreamRegular.Read(fontDataRegular, 0, (int)fontStreamRegular.Length);
            fontStreamBold.Read(fontDataBold, 0, (int)fontStreamBold.Length);

            Marshal.Copy(fontDataRegular, 0, dataRegular, (int)fontStreamRegular.Length);
            Marshal.Copy(fontDataBold, 0, dataBold, (int)fontStreamBold.Length);

            uint cFonts = 0;
            AddFontMemResourceEx(dataRegular, (uint)fontDataRegular.Length, IntPtr.Zero, ref cFonts);
            AddFontMemResourceEx(dataBold, (uint)fontDataBold.Length, IntPtr.Zero, ref cFonts);

            mFontCollection.AddMemoryFont(dataRegular, (int)fontStreamRegular.Length);
            mFontCollection.AddMemoryFont(dataBold, (int)fontStreamBold.Length);

            fontStreamRegular.Close();
            fontStreamBold.Close();
            Marshal.FreeCoTaskMem(dataRegular);
            Marshal.FreeCoTaskMem(dataBold);

            // Custom fonts loaded. Time to assign them to all the labels of the program.

            Invoke((MethodInvoker)delegate
            {

                lblMmr.Font = new Font(mFontCollection.Families[0], 24F);
                lblAddedMmr.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblRankName.Font = new Font(mFontCollection.Families[0], 24F);
                lblGamesPlayedCount.Font = new Font(mFontCollection.Families[0], 14.25F);
                lblGamesPlayed.Font = new Font(mFontCollection.Families[1], 14.25F);
                lblRating.Font = new Font(mFontCollection.Families[1], 24F);
                lblPlaylistName.Font = new Font(mFontCollection.Families[0], 27.75F);
                lblPlaylist.Font = new Font(mFontCollection.Families[0], 24F);
                lblTrackerLastUpdateValue.Font = new Font(mFontCollection.Families[1], 15.75F);
                lblTrackerLastUpdateTag.Font = new Font(mFontCollection.Families[0], 15.75F);
                lblTrackerGlobalWorstStreakValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalBestStreakValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalCurrentStreakValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalLosesValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalWinsValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalGamesPlayedValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerGlobalMmrRatioValue.Font = new Font(mFontCollection.Families[0], 24F);
                lblTrackerGlobalWorstStreakTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalBestStreakTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalCurrentStreakTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalLosesTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalWinsTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalGamesPlayedTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerGlobalMmrRatioTag.Font = new Font(mFontCollection.Families[1], 24F);
                lblTrackerGlobalTag.Font = new Font(mFontCollection.Families[0], 27.75F);
                lblTrackerLastgameRankDiv.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerLastgameRankValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerLastgameRankTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerLastgamePlaylistValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerLastgamePlaylistTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerLastgameMmrWonLostValue.Font = new Font(mFontCollection.Families[0], 20.25F);
                lblTrackerLastgameMmrWonLostTag.Font = new Font(mFontCollection.Families[1], 20.25F);
                lblTrackerLastgameTag.Font = new Font(mFontCollection.Families[0], 27.75F);

            });

        }

        public void InitLabelsAndImages()
        {

            ResetTrackerGUI();
            lblUser.Text = "";
            toolStripStatusLabel.Text = "Tracker status: Starting...";
            btnTrackerReset.Visible = false;
            lblRankName.Text = "YOUR RANK";
            lblMmr.Text = "";
            lblAddedMmr.Text = "";
            lblGamesPlayedCount.Text = "";
            lblVersion.Text = version;
            picOverviewRank.Image = Resources._0;
            listBoxOverview.Visible = false;
            PopulateTimelineTab();
            PopulateSessionSummaryTab(true);

        }

        public void CheckUpdates(bool onStartup)
        {

            String latestVersion = GetLatestVersion();

            if (!latestVersion.Equals(version))
            {
                if(!latestVersion.Equals("fail"))
                {
                    MessageBox.Show(@"Version outdated! This version: " + version + ". New version: " + GetLatestVersion() + "\n\nDownload latest version at:\nlandarltracker.com", " Landa\'s RL Tracker: Update checker",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else if(!onStartup)
                {
                    MessageBox.Show(@"There was a problem checking for a new version, try again later.", " Landa\'s RL Tracker: Update checker",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if(!onStartup)
                {
                    MessageBox.Show("Tracker is up to date!", " Landa\'s RL Tracker: Update checker",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        public string GetLatestVersion()
        {

            const string GITHUB_API = "https://api.github.com/repos/BlancoLanda/LandasRLTracker/releases";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Landas RL Tracker");
            Uri uri = new Uri(GITHUB_API);
            string releases = "";
            try
            {
                releases = webClient.DownloadString(uri);
            } catch (WebException)
            {
                return "fail";
            }
            
            JArray a = JArray.Parse(releases);
            string tagName = "";
            foreach (JObject content in a.Children<JObject>())
            {
                foreach (JProperty prop in content.Properties())
                {
                    string name = prop.Name.ToString();
                    if (name.Equals("tag_name"))
                    {
                        tagName = prop.Value.ToString();
                        return tagName;
                    }
                }
            }
            return tagName;
        }

        public void InitAllStats(bool reset)
        {

            if (reset == false)
            {
                statsPerPlaylist = new SortedDictionary<string, List<string>>();
                initialStatsPerPlaylist = new SortedDictionary<string, List<string>>();

                GetInitialPlaylists();
                UpdatePlaylistStats();

            } else
            {
                foreach (var playlist in statsPerPlaylist.Keys)
                {
                    statsPerPlaylist[playlist][4] = "0";
                    statsPerPlaylist[playlist][5] = "0";
                    statsPerPlaylist[playlist][6] = "0";
                }

                foreach (var playlist in initialStatsPerPlaylist.Keys)
                {
                    initialStatsPerPlaylist[playlist][0] = statsPerPlaylist[playlist][0];
                    initialStatsPerPlaylist[playlist][1] = statsPerPlaylist[playlist][1];
                    initialStatsPerPlaylist[playlist][2] = statsPerPlaylist[playlist][2];
                    initialStatsPerPlaylist[playlist][3] = statsPerPlaylist[playlist][3];
                    initialStatsPerPlaylist[playlist][4] = statsPerPlaylist[playlist][4];
                    initialStatsPerPlaylist[playlist][5] = "0";
                    initialStatsPerPlaylist[playlist][6] = "0";
                }

            }

            sessionTotalLoses = 0;
            sessionTotalWins = 0;
            sessionTotalMmrRatio = 0;
            sessionTotalGames = 0;
            sessionCurrentStreak = 0;
            sessionLongestWStreak = 0;
            sessionLongestLStreak = 0;

            AppendStatsToFiles();

            ResetTrackerGUI();
            ClearGrids();

            timelineList = new BindingList<Timeline>();

            PopulateOverviewTab();
            PopulateSessionSummaryTab(false);
            PopulateTimelineTab();

            Invoke((MethodInvoker)delegate
            {

                lblUser.Text = "User:";
                lblSteamControl.Visible = true;
                lblSteamControl.Text = steamNickname;
                btnTrackerReset.Visible = true;
                listBoxOverview.Visible = true;

            });

        }


        public void ResetTrackerGUI()
        {
            Invoke((MethodInvoker)delegate
            {

                lblAddedMmr.Text = "";

                lblTrackerGlobalMmrRatioValue.ForeColor = Color.Blue;
                lblTrackerGlobalMmrRatioValue.Text = "+0";
                lblTrackerGlobalGamesPlayedValue.Text = "0";
                lblTrackerGlobalWinsValue.Text = "0";
                lblTrackerGlobalLosesValue.Text = "0";
                lblTrackerGlobalCurrentStreakValue.Text = "0";
                lblTrackerGlobalBestStreakValue.Text = "0";
                lblTrackerGlobalWorstStreakValue.Text = "0";

                lblTrackerLastgamePlaylistValue.Text = "";
                lblTrackerLastgameMmrWonLostValue.Text = "";
                lblTrackerLastgameRankValue.Text = "";
                lblTrackerLastgameRankDiv.Text = "";
                picTrackerLastgameRank.Image = Resources._0;

                lblTrackerLastUpdateValue.Text = "";

            });
        }

        public void ClearGrids()
        {
            Invoke((MethodInvoker)delegate
            {

                dataGridSessionsummary.Rows.Clear();
                dataGridSessionsummary.Refresh();
                dataGridSessiontimeline.Rows.Clear();
                dataGridSessiontimeline.Refresh();

            });
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }

        public void CheckRLState()
        {

            while (true)
            {
                RLProcess = Process.GetProcessesByName("RocketLeague");
                if (RLProcess.Length == 0)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel.Text = "Tracker status: OFF (Rocket League is closed)";
                        lblUser.Text = "";
                        lblSteamControl.Visible = false;
                    });
                    isRlOn = false;
                    trackerThreadNeeded = true;
                } else
                {
                    isRlOn = true;
                    FileInfo rlLogInfo = new FileInfo(RLLogPath);
                    if (IsFileLocked(rlLogInfo))
                    {
                        logBeingWritten = true;
                    }
                    else
                    {
                        logBeingWritten = false;
                    }
                }
                System.Threading.Thread.Sleep(3000);
            }

        }

        public void LoopCheckRLProccess()
        {

            CheckUpdates(true);

            while (true)
            {

                while (isRlOn == false || logBeingWritten == false)
                {
                    System.Threading.Thread.Sleep(3000);
                }

                if (!SetSteamId().Equals(currentData))
                {
                    trackerThreadNeeded = true;

                    steamId = SetSteamId();
                    steamNickname = SetNicknameFromId(steamId);

                    InitAllStats(false);

                }

                if (trackerThreadNeeded)
                {
                    tLiveTracking = new System.Threading.Thread(StartLiveTracking)
                    {
                        IsBackground = true
                    };
                    tLiveTracking.Start();

                    Invoke((MethodInvoker)delegate
                    {

                        lblUser.Text = "User:";
                        lblSteamControl.Visible = true;
                        lblSteamControl.Text = steamNickname;
                        toolStripStatusLabel.Text = "Tracker status: Running...";

                    });

                    trackerThreadNeeded = false;

                }

                System.Threading.Thread.Sleep(5000);

            }
        }

        public void PopulateOverviewTab()
        {
            BindingList<string> playList = new BindingList<string>();

            foreach (KeyValuePair<string, List<string>> entry in statsPerPlaylist)
            {
                string playlistName = MapPlaylistName(int.Parse(entry.Key));
                playList.Add(playlistName);
            }

            Invoke((MethodInvoker)delegate
            {
                listBoxOverview.DataSource = playList;
                listBoxOverview.SelectedIndex = GetPlaylistIndexOfMostPlayedPlaylist();
            });
        }

        public void PopulateSessionSummaryTab(bool empty)
        {
            var PlaylistSummary = new BindingList<PlaylistSummary>();

            if(empty == false)
            {
                foreach (var playlist in statsPerPlaylist.Keys)
                {
                    // Check if final number of matches played equals to initial one. If true, it means the playlist was active in this session. If false, playlist not active, and ignores it.
                    if (int.Parse(initialStatsPerPlaylist[playlist][5]) + int.Parse(initialStatsPerPlaylist[playlist][6]) == int.Parse(statsPerPlaylist[playlist][5]) + int.Parse(statsPerPlaylist[playlist][6]))
                    {
                        continue;
                    }
                    else
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

                        PlaylistSummary.Add(new PlaylistSummary { Playlist = name, Games = matchesPlayed, W = wins, L = loses, MMR = mmrRatio, Initial_Rank = initialRankName, Current_Rank = currentRankName });
                    }
                }
            }

            var source = new BindingSource(PlaylistSummary, null);

            Invoke((MethodInvoker)delegate
            {
                dataGridSessionsummary.DataSource = source;
                dataGridSessionsummary.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridSessionsummary.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessionsummary.Columns[1].Width = 45;
                dataGridSessionsummary.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridSessionsummary.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessionsummary.Columns[3].Width = 30;
                dataGridSessionsummary.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessionsummary.Columns[4].Width = 40;
                dataGridSessionsummary.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridSessionsummary.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridSessionsummary.Update();
                dataGridSessionsummary.Refresh();

            });
        }

        public void PopulateTimelineTab()
        {

            var source = new BindingSource(timelineList, null);

            Invoke((MethodInvoker)delegate
            {
                dataGridSessiontimeline.DataSource = source;
                dataGridSessiontimeline.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridSessiontimeline.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessiontimeline.Columns[1].Width = 120;
                dataGridSessiontimeline.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessiontimeline.Columns[2].Width = 50;
                dataGridSessiontimeline.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessiontimeline.Columns[3].Width = 40;
                dataGridSessiontimeline.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridSessiontimeline.Columns[4].Width = 180;
                dataGridSessiontimeline.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridSessiontimeline.Update();
                dataGridSessiontimeline.Refresh();
            });

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
                Console.Error.WriteLine(" Steam is opened but not logged in. Log in, open Rocket League and try again! Exiting program...");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(1);
            }

            return name;
        }

        public void GetInitialPlaylists()
        {

            Invoke((MethodInvoker)delegate
            {
                toolStripStatusLabel.Text = "Tracker status: Getting rank data... (Skip RL intro!)";
            });

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
                                playlist = value.Replace("Playlist=", "").Replace(" ", string.Empty);
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

        public void UpdatePlaylistStats()
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
                                    else if (line.Contains("Tier=19"))
                                    {
                                        // Avoid showing "Div I" for Grand Champs.
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

            currentData = steamId;

        }

        public void StartLiveTracking()
        {

            var initialFileSize = new FileInfo(RLLogPath).Length;
            var lastReadLength = initialFileSize - 1024;
            if (lastReadLength < 0) lastReadLength = 0;

            while(isRlOn)
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
                                                    playlist = value.Replace("Playlist=", "").Replace(" ", string.Empty);
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
                                                        PopulateSessionSummaryTab(false);
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
                                                        string outcome;
                                                        int mmrWonOrLost = numericMmr - numericPreviousMmr;
                                                        string playlistSessionWins = statsPerPlaylist[playlist][5];
                                                        string playlistSessionLoses = statsPerPlaylist[playlist][6];
                                                        string mmrRatio = (int.Parse(statsPerPlaylist[playlist][4]) + mmrWonOrLost).ToString();
                                                        sessionTotalMmrRatio = sessionTotalMmrRatio + mmrWonOrLost;
                                                        sessionTotalGames++;
                                                        if (mmrWonOrLost > 0)
                                                        {
                                                            // Match finished with a win.
                                                            outcome = "W";
                                                            playlistSessionWins = (int.Parse(statsPerPlaylist[playlist][5]) + 1).ToString();
                                                            sessionTotalWins++;

                                                            if (sessionCurrentStreak < 0)
                                                            {
                                                                // End of current losing streak.
                                                                sessionCurrentStreak = 1;
                                                            }
                                                            else
                                                            {
                                                                // Current winning streak keeps going.
                                                                sessionCurrentStreak++;
                                                            }

                                                            if (sessionCurrentStreak > sessionLongestWStreak)
                                                            {
                                                                sessionLongestWStreak = sessionCurrentStreak;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            // Match finished with a lose.
                                                            outcome = "L";
                                                            playlistSessionLoses = (int.Parse(statsPerPlaylist[playlist][6]) + 1).ToString();
                                                            sessionTotalLoses++;

                                                            if (sessionCurrentStreak > 0)
                                                            {
                                                                // End of current winning streak.
                                                                sessionCurrentStreak = -1;
                                                            }
                                                            else
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

                                                        // This piece of code updates "Tracker" tab in GUI.

                                                        Invoke((MethodInvoker)delegate
                                                        {

                                                            if (tierChange.Equals("up"))
                                                            {
                                                                lblTrackerLastgameRankDiv.ForeColor = Color.Green;
                                                                lblTrackerLastgameRankDiv.Text = "Rank up!";
                                                            }
                                                            else if (tierChange.Equals("down"))
                                                            {
                                                                lblTrackerLastgameRankDiv.ForeColor = Color.Red;
                                                                lblTrackerLastgameRankDiv.Text = "Rank down!";   
                                                            }
                                                            else if (divisionChange.Equals("up"))
                                                            {
                                                                lblTrackerLastgameRankDiv.ForeColor = Color.Green;
                                                                lblTrackerLastgameRankDiv.Text = "Div up!";
                                                            }
                                                            else if (divisionChange.Equals("down"))
                                                            {
                                                                lblTrackerLastgameRankDiv.ForeColor = Color.Red;
                                                                lblTrackerLastgameRankDiv.Text = "Div down!";
                                                            } else
                                                            {
                                                                // If not rank/tier up/down, then there is no message.
                                                                lblTrackerLastgameRankDiv.Text = "";
                                                            }

                                                            lblTrackerLastgamePlaylistValue.Text = MapPlaylistName(int.Parse(playlist));

                                                            if (mmrWonOrLost < 0)
                                                            {
                                                                lblTrackerLastgameMmrWonLostValue.ForeColor = Color.Red;
                                                            }
                                                            else
                                                            {
                                                                lblTrackerLastgameMmrWonLostValue.ForeColor = Color.Green;
                                                            }

                                                            lblTrackerLastgameMmrWonLostValue.Text = mmrWonOrLost.ToString("+0;-#");
                                                            lblTrackerLastgameRankValue.Text = MapTierName(int.Parse(stats[1])) + " " + MapDivisionName(int.Parse(stats[2])) + " [" + CalculateRescaledMmr(decimal.Parse(stats[0], CultureInfo.InvariantCulture)).ToString() + "]";
                                                            picTrackerLastgameRank.Image = (Image)Resources.ResourceManager.GetObject(statsPerPlaylist[playlist][1]);

                                                            if (sessionTotalMmrRatio < 0)
                                                            {
                                                                lblTrackerGlobalMmrRatioValue.ForeColor = Color.Red;
                                                            }
                                                            else if (sessionTotalMmrRatio > 0)
                                                            {
                                                                lblTrackerGlobalMmrRatioValue.ForeColor = Color.Green;
                                                            }
                                                            else
                                                            {
                                                                lblTrackerGlobalMmrRatioValue.ForeColor = Color.Blue;
                                                            }

                                                            lblTrackerGlobalMmrRatioValue.Text = sessionTotalMmrRatio.ToString("+0;-#");
                                                            lblTrackerGlobalGamesPlayedValue.Text = sessionTotalGames.ToString();
                                                            lblTrackerGlobalWinsValue.Text = sessionTotalWins.ToString();
                                                            lblTrackerGlobalLosesValue.Text = sessionTotalLoses.ToString();
                                                            lblTrackerGlobalCurrentStreakValue.Text = GetStreakStringByInt(sessionCurrentStreak);
                                                            lblTrackerGlobalBestStreakValue.Text = GetStreakStringByInt(sessionLongestWStreak);
                                                            lblTrackerGlobalWorstStreakValue.Text = GetStreakStringByInt(sessionLongestLStreak);

                                                            lblTrackerLastUpdateValue.Text = DateTime.Now.ToString();

                                                            // This piece of code updates rank in "rank overview" tab in GUI.

                                                            lblPlaylistName.Text = MapPlaylistName(int.Parse(playlist));
                                                            lblRankName.Text = MapTierName(int.Parse(statsPerPlaylist[playlist][1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2]));
                                                            lblMmr.Text = CalculateRescaledMmr(decimal.Parse(statsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString();

                                                            if (int.Parse(statsPerPlaylist[playlist][4]) < 0)
                                                            {
                                                                lblAddedMmr.ForeColor = Color.Red;
                                                            }
                                                            else if (int.Parse(statsPerPlaylist[playlist][4]) > 0)
                                                            {
                                                                lblAddedMmr.ForeColor = Color.Green;
                                                            }
                                                            else
                                                            {
                                                                lblAddedMmr.ForeColor = Color.Blue;
                                                            }

                                                            lblAddedMmr.Text = "(" + int.Parse(statsPerPlaylist[playlist][4]).ToString("+0;-#") + ")";

                                                            lblGamesPlayedCount.Text = statsPerPlaylist[playlist][3];
                                                            picOverviewRank.Image = (Image)Resources.ResourceManager.GetObject(statsPerPlaylist[playlist][1]);

                                                        });

                                                    PopulateSessionSummaryTab(false);
                                                    timelineList.Add(new Timeline { No = sessionTotalGames, Playlist = MapPlaylistName(int.Parse(playlist)), Result = outcome, MMR = mmrWonOrLost.ToString("+0;-#"), Rank = MapTierName(int.Parse((statsPerPlaylist[playlist])[1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2])) + " (" + CalculateRescaledMmr(decimal.Parse(mmr, CultureInfo.InvariantCulture)).ToString() + " MMR)", Time = DateTime.Now.ToString() });
                                                    PopulateTimelineTab();
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
                
            }
        }

        public void AppendStatsToFiles()
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

        public void AppendStatsToFiles(string playlist)
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

        public decimal GetWinPercentage(int wins, int loses)
        {
            int totalGames = wins + loses;

            if (totalGames == 0)
            {
                return 0;
            }
            else
            {

                decimal winPercentage = ((decimal)wins / (decimal)totalGames) * 100;
                return winPercentage;

            }

        }

        public string GetStreakStringByInt(int streak)
        {
            string streakString = "";

            if (streak < 0)
            {
                streak = Math.Abs(streak);
                streakString = streak.ToString() + "L";
            }
            else if (streak == 0)
            {
                streakString = streak.ToString();
            }
            else
            {
                streakString = streak.ToString() + "W";
            }

            return streakString;

        }

        public static int CalculateRescaledMmr(decimal mmr)
        {
            int rescaledMmr = Convert.ToInt32(Math.Round(((20 * mmr) + 100), 0));
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

        public static int MapPlaylistNameReversed(string playlistName)
        {
            var map = new Dictionary<string, int>()
            {
                { "Solo Duel (1vs1)", 10 },
                { "Doubles (2vs2)", 11 },
                { "Solo Standard (3vs3)", 12 },
                { "Standard (3vs3)", 13 },
                { "Hoops", 27 },
                { "Rumble", 28  },
                { "Dropshot", 29 },
                { "Snow Day", 30 }
            };

            return map[playlistName];

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

        public static int MapPlaylistToIndex(int playlist)
        {
            var map = new Dictionary<int, int>()
            {
                { 10 , 0 },
                { 11, 1 },
                { 12, 2 },
                { 13, 3 },
                { 27, 4 },
                { 28, 5 },
                { 29, 6 },
                { 30, 7 }
            };

            return map[playlist];

        }

        public int GetPlaylistIndexOfMostPlayedPlaylist()
        {

            var ordered = statsPerPlaylist.OrderBy(x => int.Parse(x.Value[3]));
            string key = ordered.Last().Key;

            switch (int.Parse(key))
            {
                case 10:
                    return 0;
                case 11:
                    return 1;
                case 12:
                    return 2;
                case 13:
                    return 3;
                case 27:
                    return 4;
                case 28:
                    return 5;
                case 29:
                    return 6;
                case 30:
                    return 7;
                default:
                    return 0;
            }   
                
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPlaylist;

            selectedPlaylist = (string)listBoxOverview.Items[listBoxOverview.SelectedIndex];
            string playlist = MapPlaylistNameReversed(selectedPlaylist).ToString();

            Invoke((MethodInvoker)delegate
            {
                lblPlaylistName.Text = selectedPlaylist;
                lblRankName.Text = MapTierName(int.Parse(statsPerPlaylist[playlist][1])) + " " + MapDivisionName(int.Parse((statsPerPlaylist[playlist])[2]));
                lblMmr.Text = CalculateRescaledMmr(decimal.Parse(statsPerPlaylist[playlist][0], CultureInfo.InvariantCulture)).ToString();

                // Only update added MMR label if there were played matches in this playlist (wins + loses > 0)

                if (int.Parse(statsPerPlaylist[playlist][5]) + int.Parse(statsPerPlaylist[playlist][6]) > 0)
                {
                    if (int.Parse(statsPerPlaylist[playlist][4]) < 0)
                    {
                        lblAddedMmr.ForeColor = Color.Red;
                    }
                    else if (int.Parse(statsPerPlaylist[playlist][4]) > 0)
                    {
                        lblAddedMmr.ForeColor = Color.Green;
                    } else
                    {
                        lblAddedMmr.ForeColor = Color.Blue;
                    }

                    lblAddedMmr.Text = "(" + int.Parse(statsPerPlaylist[playlist][4]).ToString("+0;-#") + ")";
                }
                else
                {
                    lblAddedMmr.Text = "";
                }
                lblGamesPlayedCount.Text = statsPerPlaylist[playlist][3];
                picOverviewRank.Image = (Image)Resources.ResourceManager.GetObject(statsPerPlaylist[playlist][1]);
            });

        }

        private void BtnTrackerReset_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                btnTrackerReset.Text = "Restarting...";
                btnTrackerReset.Refresh();
            });
            
            InitAllStats(true);

            Invoke((MethodInvoker)delegate
            {
                btnTrackerReset.Text = "Restart session";
                btnTrackerReset.Refresh();
            });
            
        }

        private void OpenStreamerFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"StreamerKit");
        }

        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdates(false);
        }

        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://landarltracker.com");
        }

        private void LandasTwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/LandaRLTracker/"); 
        }

        private void HowToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Open RL. Tracker will obtain data after you skip the intro.\n2. Start playing.\n3. Don't leave a match until 'BLUE/ORANGE TEAM WINS' message appears. If you leave early, match wont be logged.", " Landa\'s RL Tracker: How to use",
MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void RunOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (runOnStartupToolStripMenuItem.Checked)
            {
                // It was disabled. Enabled now.
                Settings.Default.runOnStartupChecked = true;
            }
            else
            {
                // It was enabled. Disabled now.
                Settings.Default.runOnStartupChecked = false;
            }

            SetStartup();

        }

        private void MinimizeOnLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (minimizeOnLaunchToolStripMenuItem.Checked)
            {
                // It was disabled. Enabled now.
                Settings.Default.minimizeOnLaunchChecked = true;
            }
            else
            {
                // It was enabled. Disabled now.
                Settings.Default.minimizeOnLaunchChecked = false;
            }
        }

        private void MinimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (minimizeToTrayToolStripMenuItem.Checked)
            {
                // It was disabled. Enabled now.
                Settings.Default.minimizeToTrayChecked = true;
            }
            else
            {
                // It was enabled. Disabled now.
                Settings.Default.minimizeToTrayChecked = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Settings.Default.minimizeToTrayChecked)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    notifyIcon.Visible = true;
                }
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
        }

        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (Settings.Default.runOnStartupChecked)
                rk.SetValue(@"Landa's RL Tracker", Application.ExecutablePath);
            else
                rk.DeleteValue(@"Landa's RL Tracker", false);

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
