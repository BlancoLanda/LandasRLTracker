# Landa's RL Tracker

## About

Landa's RL Tracker is a tool that allows tracking Rocket League gaming sessions. It gives live detailed information about matches being played for any playlist. Only Steam platform running in a Windows 7+ machine is supported.

Information is obtained using Rocket League local logfiles (Psyonix API is closed from public access and I don't have a key). No third party queries are made. It checks the logs every second and parses all the MMR updates it throws.

It's plug&play: Once tracker is opened, you don't have to do anything. Play your matches and enjoy the tracking! 💃 

**Only Windows 7+ and Steam RL accounts supported.**

## Information displayed

Landa's RL Tracker implements the following features:

- **Rank overview:** Detailed rank information for every playlist.

![Image](https://i.imgur.com/x3KT4vs.png)

- **Tracker:** Live tracker, with Wins/Loses, rank and MMR information for every match and the whole session.

![Image](https://i.imgur.com/GXcinr4.png)

- **Playlist summary:** A summary of all the stats for every playlist in the current session.

![Image](https://i.imgur.com/g2484Wb.png)

- **Session timeline:** Table containing a timeline with all played matches in the session.

![Image](https://i.imgur.com/yWhKtMd.png)

  
## Tool for streamers
  
  This executable generates a folder called 'StreamerKit'. Inside it, you have a set of files with all the information of your matches you may need. Add whatever stat file you want to show in your streaming! **Everything updates in real-time whenever a match ends**
  
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

Basically only one **BIG problem**: Rocket League only logs matches after the winner is announced. That means, it won't be logged if:

1. If one team surrenders, and you leave BEFORE the winner announcement (i.e. forfeiting during a goal replay and leaving before the replay end).
2. When a goal is scored in minute 00:00 (or when scored in overtime) and you leave before the winner announcement.
3. If you leave without forfeiting (Force quit with game ban). Also in team playlists, if you leave after one of your teammates left without forfeiting.

That happens because RL triggers match data updates when a winner is announced. 

There's a possibility that match will be logged anyway, but **I recommend you to always wait to winner announce**!

## [Extra] In-game GUI [OverlayMCM] (Thanks to Kaientai)

User [Kaientai](https://steamcommunity.com/id/thekaientai) made an awesome AHK script, called OverlayMCM, that allows getting updates and stats in-game, you don't even need to check the console.

 ![Image](https://i.imgur.com/epfe5ma.png)
 
  **How to install it?**
  - Download source code and binaries [here](https://github.com/BlancoLanda/LandasRLTracker/releases/download/v1.4.0/OverlayMCM.zip)
  - Extract its contents in the same folder where _LandasRLTracker.exe_ is.
  - Run _LandasRLTracker.exe_ as always.
  - Now, also run _OverlayMCM.exe_ in the background. Ready!
  
  (**Note:** Source code is located in _OverlayMCM.exe_. You can make any change there (for example: Language), then you need software like [Autohotkey](https://www.autohotkey.com/download/) to compile the .ahk file to .exe)
 
 **How it works?**
 
 - When a match ends, an overlay with stats of the current playlists appears during 5 seconds.
 - You can check the overlay whenever you want by pressing F1 key.
 - You can leave the program by pressing F12 key.

## Is it legal and safe?

This tracker is safe and legal, you will never get a Rocket League crash from this, because it does NEVER interact with Rocket League client. It only gets live information from Rocket League text logs with 'Read only' flag.

## Need help?

If you find any bug, need any help or want to contact me for anything related with this project please, send me a message! You can reach me at [Steam](https://steamcommunity.com/id/blancolanda/), [Twitter](https://twitter.com/LandaRLTracker) or [Reddit](https://www.reddit.com/user/Blancolanda)!

## Donations

If you are enjoying the tracker and want to collaborate, you can also make me a little donation. Thank you very much!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VA22BR3GLBECC&source=url)

