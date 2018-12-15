# Landa's RL Tracker

## About

Landa's RL Tracker allows tracking Rocket League gaming sessions. It gives live detailed information about matches being played for any playlist.

Information is obtained using Rocket League local logfiles. No third party queries are made. It checks the logs every 20 seconds and parses all the MMR information it contains.

The only requirement is having your Rocket League client opened... The tracker does the rest!

## Information displayed

Landa's RL Tracker displays the following information inside a console:
- Initially, a list of the playlists where you are have an MMR number assigned, with the following information for each playlist:
  * Playlist name (1s, 2s, 3s, ...)
  * MMR number (and rank name + division associated)
  * Total games played

- When a match from any playlist is determined, it updates with the following information:
  * Updated MMR (and rank name + division associated) in that playlist.
  * MMR ratio of the session for the current playlist.
  * Total MMR ratio of the session including all playlists.
  * Rank up/down and tier up/down notifications.
  * Total games played and W/L ratio of the session for the current playlist.
  * Total games played and W/L of the session including all playlists.
  
## Tool for streamers
  
  Are you a streamer and do you want to have an overlay with your live stats, like MMR or W/L ratios, but you're tired of updating them manually after each match? Then Landa's RL Tracker is the tool you're looking for.
  This executable generates a folder called 'StreamerKit'. Inside it, you have a set of files with all the information of your matches you may need!
  
  ###### EXAMPLE: Importing session MMR ratio of 'Standard' playlist to OBS Studio
  
  1. On "Sources" window, click the "+" (add) button and select "Text (GDI+)".
  2. Name your new Source, e.g., '3s_MMR_Ratio'
  3. Check "Read from file", and select "StreamerKit/Standard (3vs3)/mmr_session_balance.txt" file.
  4. DONE! Now simply choose the font, size, colors and decorate it as you want. This data will automatically update when a match is completed.
  
## Need help?

If you need any help or want to contact me for anything related with this project, you can reach me at [Steam](https://steamcommunity.com/id/blancolanda/) or [Twitter](https://twitter.com/BlancoLanda)!