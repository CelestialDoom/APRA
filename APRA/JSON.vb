Module JSON

    '<---Starts here
    Public Class Station
        Public Property id As Integer
        Public Property name As String
        Public Property shortcode As String
        Public Property genre As String
        Public Property category As String
        Public Property affiliation As String
        Public Property image_url As String
        Public Property web_url As String
        Public Property sort_order As Integer
        Public Property player_url As String
        Public Property request_url As String
        Public Property stream_url As String

    End Class

    Public Class Listeners
        Public Property current As Integer
        Public Property unique As Integer
        Public Property total As Integer
    End Class

    Public Class Bronytunes
        Public Property id As String
        Public Property hash As String
        Public Property created As String
        Public Property updated As String
        Public Property artist As String
        Public Property title As String
        Public Property album As String
        Public Property description As String
        Public Property lyrics As String
        Public Property web_url As String
        Public Property image_url As String
        Public Property download_url As String
        Public Property youtube_url As String
        Public Property purchase_url As String
    End Class

    Public Class External
        Public Property bronytunes As Bronytunes
    End Class

    Public Class CurrentSong
        Public Property id As String
        Public Property text As String
        Public Property artist As String
        Public Property title As String
        Public Property image_url As String
        Public Property external As External
    End Class

    Public Class Song
        Public Property id As String
        Public Property text As String
        Public Property artist As String
        Public Property title As String
        Public Property image_url As String
    End Class

    Public Class Stream
        Public Property id As Integer
        Public Property name As String
        Public Property url As String
        Public Property type As String
        Public Property is_default As Boolean
        Public Property status As String
        Public Property bitrate As String
        Public Property format As String
        Public Property listeners As Listeners
        Public Property current_song As CurrentSong
        Public Property song_history As SongHistory()
    End Class

    Public Class SongHistory
        Public Property played_at As Integer
        Public Property song As Song
    End Class

    Public Class Result
        Public Property status As String
        Public Property station As Station
        Public Property current_song As CurrentSong
        Public Property song_history As SongHistory()
        Public Property listeners As Listeners
        Public Property event_1 As Object()
        Public Property cache As String

    End Class

    Public Class Example
        Public Property status As String
        Public Property result As Result
    End Class
    '<--Ends Here!

    '<--StationListing
    Public Class Stream_List
        Public Property id As Integer
        Public Property name As String
        Public Property url As String
        Public Property type As String
        Public Property is_default As Boolean
    End Class

    Public Class Result_List
        Public Property id As Integer
        Public Property name As String
        Public Property shortcode As String
        Public Property genre As String
        Public Property category As String
        Public Property affiliation As String
        Public Property image_url As String
        Public Property web_url As String
        Public Property twitter_url As String
        Public Property irc As String
        Public Property sort_order As Integer
        Public Property streams As Stream_List()
        Public Property default_stream_id As Integer
        Public Property stream_url As String
        Public Property player_url As String
        Public Property request_url As String
    End Class

    Public Class StationList
        Public Property status As String
        Public Property result As Result_List()
    End Class
    '<--Ends Here!

End Module
