# Landa's RL Tracker

## About

Landa's RL Tracker is a tool written in C# that allows tracking Rocket League gaming sessions. It gives live detailed information about matches being played for any playlist. Only Steam platform running in a Windows machine is supported.

Information is obtained using Rocket League local logfiles (Psyonix API is closed from public access). No third party queries are made. It checks the logs every second and parses all the MMR information it contains.

## Usage

The only requirement is having your Rocket League client opened BEFORE using Landa's RL Tracker.
When using it, don't close the console until you finish your sessions. It is responsible of monitoring updates.

## Information displayed

Landa's RL Tracker displays the following information inside a console:
- Initially, a list of the playlists where you are have an MMR number assigned, with the following information for each playlist:
  * Playlist name (1s, 2s, 3s, ...)
  * MMR
  * Rank name and division for each playlist
  * Total games played
  
![Image](https://i.imgur.com/femzy6F.png)

- When a match from any playlist is determined, it updates with the following information:
  * MMR lost/won in that played match.
  * Rank up/down and tier up/down notifications.  
  * Updated MMR (and rank name + division associated) in that playlist.
  * MMR ratio of the session for the current playlist.
  * Total games played and W/L ratio of the session for the current playlist.
  * Total MMR ratio of the session including all playlists.
  * Total games played and W/L of the session including all playlists.
  
![Image](https://i.imgur.com/2gTnvYq.png)
  
## Tool for streamers
  
  Are you a streamer and do you want to have an overlay with your live stats, like MMR or W/L ratios, but you're tired of updating them manually after each match? Then Landa's RL Tracker is the tool you're looking for.
  This executable generates a folder called 'StreamerKit'. Inside it, you have a set of files with all the information of your matches you may need. Add whatever stat file you want to show in your streaming!
  
  **How it looks:**
  
  ![Image](https://media.discordapp.net/attachments/518865179274903563/518871407724068884/Stream.PNG)
  
  ###### EXAMPLE: Importing session MMR ratio of 'Standard' playlist to OBS Studio
  
  1. On "Sources" window, click the "+" (add) button and select "Text (GDI+)".
  2. Name your new Source, e.g., '3s_MMR_Ratio'
  3. Check "Read from file", and select "StreamerKit/Standard (3vs3)/mmr_session_balance.txt" file.
  4. DONE! Now simply choose the font, size, colors and decorate it as you want. This data will automatically update when a match is completed.

## Download

Head to the [releases page](https://github.com/BlancoLanda/LandasRLTracker/releases) for the executable download link.

## Known issues

Actually, reading MMR changes through RL logs is not perfect. There are two cases where likely the tracker won't work:
1. If one team surrenders, and you leave BEFORE the winner announcement (i.e. forfeiting during a goal replay and leaving before the replay end).
2. When a goal is scored in minute 00:00 and you leave before the winner announcement.
That's because RL logs wins and MMR changes when a winner is announced. If you leave a match before that, there are high chances that it won't be logged.
I'll try to look for some workaround, but at the moment this is the situation!

## Need help?

If you need any help or want to contact me for anything related with this project, you can reach me at [Steam](https://steamcommunity.com/id/blancolanda/) or [Twitter](https://twitter.com/BlancoLanda)!
