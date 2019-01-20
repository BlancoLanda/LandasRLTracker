# Landa's RL Tracker

## About

Landa's RL Tracker is a command-line tool written in C# that allows tracking Rocket League gaming sessions. It gives live detailed information about matches being played for any playlist. Only Steam platform running in a Windows 7+ machine is supported.

Information is obtained using Rocket League local logfiles (Psyonix API is closed from public access). No third party queries are made. It checks the logs every second and parses all the MMR information it contains.

## Usage

This program requires .NET Framework v4.6.1+, normally you'll already have the latest version installed. Anyway, you can download it at: https://dotnet.microsoft.com/download/dotnet-framework-runtime

The only requirement is having the tracker opened while playing Rocket League.

**Keys (Console instance have to be selected for catching the key press):**

**R:** Reset stats without exiting the program (MMR and W/L global ratios and ratios for every playlist are reset to 0). 

## Information displayed

Landa's RL Tracker displays the following information inside a console:
- Initially, a list of the playlists where you are have an MMR number assigned, with the following information for each playlist:
  * Playlist name (1s, 2s, 3s, ...).
  * MMR.
  * Rank name and division for each playlist.
  * Total games played.
  
![Image](https://i.imgur.com/femzy6F.png)

- When a match from any playlist is determined, it updates with the following information:
  * MMR lost/won in that played match.
  * Rank up/down and tier up/down notifications.  
  * Updated MMR (and rank name + division associated) in that playlist.
  * MMR ratio of the session for the current playlist.
  * Total games played and W/L ratio of the session for the current playlist.
  * Total MMR ratio of the session including all playlists (GLOBAL).
  * Total games played and W/L of the session including all playlists (GLOBAL).
  * Current winning/losing streak.
  * Longest winning/losing streak of the session.
  
![Image](https://i.imgur.com/2PDWWYT.png)
  
## Tool for streamers
  
  Are you a streamer and do you want to have an overlay with your live stats, like MMR or W/L ratios, but you're tired of updating them manually after each match? Then Landa's RL Tracker is the tool you're looking for.
  This executable generates a folder called 'StreamerKit'. Inside it, you have a set of files with all the information of your matches you may need. Add whatever stat file you want to show in your streaming!
  
  ![ImageStreamerKit](https://i.imgur.com/IBWLHxi.png)
  
  **How it looks in my streaming:**
  
  ![Image](https://media.discordapp.net/attachments/518865179274903563/518871407724068884/Stream.PNG)
  
  ###### EXAMPLE: Importing session MMR ratio of 'Standard' playlist to OBS Studio
  
  1. On "Sources" window, click the "+" (add) button and select "Text (GDI+)".
  2. Name your new Source, e.g., '3s_MMR_Ratio'
  3. Check "Read from file", and select "StreamerKit/Standard (3vs3)/mmr_session_balance.txt" file.
  4. DONE! Now simply choose the font, size, colors and decorate it as you want. This data will automatically update when a match is completed.

## Download

Head to the [releases page](https://github.com/BlancoLanda/LandasRLTracker/releases) for the executable download link.

## Known issues

Actually, reading MMR changes through RL logs is not perfect. There are 3 cases where it won't be instant:
1. If one team surrenders, and you leave BEFORE the winner announcement (i.e. forfeiting during a goal replay and leaving before the replay end).
2. When a goal is scored in minute 00:00 (or when scored in overtime) and you leave before the winner announcement.
3. If you leave without forfeiting (Force quit with game ban). Also in team playlists, if you leave after one of your teammates left without forfeiting.

That happens because RL triggers win/lose & MMR data updates when a winner is announced. 

Normally, in these cases, stats won't be instantly updated just after the match, and **they get updated in the next log update, normally in the start of the next match.** That means, a little delay. There is also a small possibility that stats don't update, like that match never existed :/

## Is it legal and safe?

This tracker is safe, you will never get a Rocket League crash from this, because it does NEVER interact with Rocket League client. It only gets live information from Rocket League text logs with 'Read only' flag. Then, in the same way, its use is totally legal.

## Need help?

If you find any bug, need any help or want to contact me for anything related with this project please, send me a message! You can reach me at [Steam](https://steamcommunity.com/id/blancolanda/), [Twitter](https://twitter.com/LandaRLTracker) or [Reddit](https://www.reddit.com/user/Blancolanda)!

## Donations

If you are enjoying the tracker and want to collaborate, you can also make me a little donation. Thank you very much!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VA22BR3GLBECC&source=url)

