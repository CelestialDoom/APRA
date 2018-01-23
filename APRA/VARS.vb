Module VARS
    'This is all the variables used//
    'Variable definition starts here --->

    Public REQUEST As String
    Public REQ_TYPE As Integer
    Public IS_PLAYING As Boolean = False
    Public dt As DispatcherTimer = New DispatcherTimer()
    Public dt_conn As DispatcherTimer = New DispatcherTimer()
    Public dt_menu As DispatcherTimer = New DispatcherTimer()
    Public localSettings As Windows.Storage.ApplicationDataContainer = Windows.Storage.ApplicationData.Current.LocalSettings
    Public StationValue As Object = localSettings.Values("STORED_ID")
    Public value As Object = localSettings.Values("STORED_ID")
    Public ARTIST_HISTORY(5) As String
    Public TRACK_HISTORY(5) As String
    Public C_P_T As String = ""
    Public SelectedStationValue As Integer
    Public StoredScreen As String
    Public StoredStation As String
    Public StoredTheme As String
    Public Timer_Is_Running As Boolean = False
    Public About_Info As String = "This is a 3rd party Windows Desktop application for accessing the Ponyville Live! public API." & vbCrLf & vbCrLf & "It allows you, the user, to access the tracks being broadcast LIVE via the Ponyville Live! API." & vbCrLf & vbCrLf & "As stated, this is a 3rd Party app, and Ponyville Live! have NO hoof in the creation of said app." & vbCrLf & "Also, the creator of this app has NO control over what tracks are available." & vbCrLf & vbCrLf & "Please send any, and all, emails to:" & vbCrLf & "celestial_doom@derpymail.org" & vbCrLf & vbCrLf & "Thank you for using this app." & vbCrLf & vbCrLf & "Disclaimer:" & vbCrLf & vbCrLf & "The user of this app is responsible for ALL data used whilst using this app. The author, or Ponyville Live! accept NO liability for costs incurred." & vbCrLf & vbCrLf & "Final Note:" & vbCrLf & vbCrLf & "This app was lovingly created using Visual Studio 2015 Community Edition."

    Public About_Info_2 As String = "All Pony Radio App for Desktop" & vbCrLf & vbCrLf & "Company Name:" & vbCrLf & "ManeTech Studios" & vbCrLf & "©" & Date.Today.Year & " All Rights Reserved" & vbCrLf & vbCrLf & "Company Website:" & vbCrLf & "http://www.allponyradio.com/" & vbCrLf & vbCrLf & "Feebback:" & vbCrLf & "allponyradio@mane-frame.com"

    Public strArray() As String

    Public strArrayThemes As String() = {"Apra", "Twilight Sparkle", "Pinkie Pie", "Sunset Shimmer", "Fluttershy", "SilverLeaf", "Trixie", "Berry Punch", "Night Glider", "Applejack", "Cheerilee", "Rainbow Dash", "ShadowPony", "Sugar Belle", "Starlight Glimmer", "Princess Luna"}

    Public Is_Desktop As Boolean
    'Set to TRUE if running on desktop

    Public strArrayID() As Integer

    Public strArrayLogo() As String

    Public strArrayIDStr() As String

    Public strArrayGenre() As String

    Public strArrayColor As String() = {"Red", "Green", "Blue", "White", "Yellow", "Orange", "Pink"}

    Public SelectedIndex As Integer

    Public StreamURL As String
    'Stores the MP3 stream URL.

    Public PlayingTrack As Boolean = False
    'This just checks whether the app is playing, or not.

    Public StationIDs As String() = New String(30) {}
    'This stores the ID codes for each of the radio stations.

    Public StationNames As String() = New String(30) {}
    'This stores the NAMES for each of the radio stations.

    Public currentstation As String
    'This stores the array value for the above array.

    Public STATIONID As String
    'Stores the value from the registry key.

    Public MouseIsDown As Boolean = False
    'Checks whether the mouse button has been pressed.

    Public EDIT_MODE As Boolean = False
    'Sets the form into Edit Mode.

    Public StationURL As String
    'This is the URL for the radio station currently tuned in.

    Public CurrentlyPlaying As String
    'The name of the track currently played on the radio station.

    Public Splash As Integer = 0
    'End of variables.

    Public RequestURL As String
    'This is the URL for requesting a track (If available)

    Public WebURL As String
    'The URL for the stations' website (If available)

    'Public WMP As New WMPLib.WindowsMediaPlayer
    'This helps with the Windows Media Player (Makes life a bit easier for coding)

    Public Playing As Boolean = False
    'Used to check whether the application is playing

    Public STREAM_URL As String
    'Sets the URL for the audio player

    Public LOGO As Uri
    'Sets the station logo

    Public STATION_NAME As String
    'Sets the Station Name

    Public CURRENT_TRACK As String
    'What's the current track? Oh, it's this!

    Public CURRENT_ARTIST As String
    'Who's playing?

    Public GENRE As String
    'Station genre

    Public STATION_URL As String
    'The URL (website) of the selected station

    Public CURRENT_LISTENERS As String
    'The current number of listeners

    Public CURRENT_IMAGE As String
    'The current logo shown

    Public CURRENT_STATION_LOGO
    'The current stations logo

    Public LYRICS As String

    Public ST_ID As String

    Public CT() As String
    Public NEW_STATION As Boolean = True
    'Sets whether, or not, a new station has been selected

    Public THEME_BG_1(25) As String
    'Background theme colour

    Public THEME_BG_2(25) As String
    'Background theme colour

    Public THEME_BG_3(25) As String
    'Background theme colour

    Public THEME_BG_4(25) As String
    'Background theme colour

    Public THEME_FG_1(25) As String
    'Font theme colour

    Public THEME_FG_2(25) As String
    'Font theme colour

    Public THEME_FG_3(25) As String
    'Font theme colour

    Public THEME_FG_4(25) As String
    'Font theme colour

    Public THEME_IMAGE As Uri

    Public ThemeName(25) As String
    'Theme Name

    Public ST_OFFLINE As Boolean = False
    'Sets to TRUE when the station is offline

    Public BG_1 As Int32
    Public BG_2 As Int32
    Public BG_3 As Int32
    Public FG_1 As Int32
    Public FG_2 As Int32
    Public FG_3 As Int32

    Public UpdateMessage As String
    Public CurrentVer As String
    Public UpdateVer As String

    Public Recently_Played As String

    Public Station_Array() As String
    Public Station_Array_ID() As Integer

    Public TotalStations As Integer
    'This contains the Current Playing Track
    Public TIMER_VALUE As Integer = 15000
    'Sets the TIMER VALUE --- MUST ALWAYS STAY AT 15000
    Public m_cntr As Integer = 0
    'The following variables are just for testing, not more, nothin less.
    'Walk by, and don't look!
    Public TICKTOCK As Boolean = False
    Public debug_tmr As Integer = 0
    Public TMR As Integer = 0
    Public BETA_TEST As Boolean = True
    Public RPD_TMR As Integer = 0
    'And ends here <---
End Module
