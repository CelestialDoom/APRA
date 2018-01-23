' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.Phone.UI.Input
Imports Windows.System.Display
Imports Windows.UI.Popups
Imports Windows.Devices.Sensors
''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class BlankPage1
    Inherits Page

    Public _displayRequest As Windows.System.Display.DisplayRequest

    Sub BackPressed(sender As Object, e As BackPressedEventArgs)
        'Handles any Back button presses.
        'e.Handled = True
        ReleaseDisplay()
        Application.Current.Exit()
    End Sub

    Private Function LoadComboBoxData() As String()
        Return strArray
    End Function

    Public Sub ActivateDisplay()
        If _displayRequest Is Nothing Then _displayRequest = New Windows.System.Display.DisplayRequest()
        _displayRequest.RequestActive()
    End Sub

    Public Sub ReleaseDisplay()
        If _displayRequest Is Nothing Then Return
        _displayRequest.RequestRelease()
    End Sub

    Public Sub dispatcherTimer_MENU_Tick(ByVal sender As Object, ByVal e As EventArgs)
        'Countdown timer for when the menu is open, if counter is equal to 4, then the menu closes.
        m_cntr = m_cntr + 1
        If m_cntr = 4 Then
            m_cntr = 0
        End If
    End Sub

    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        TMR += 1
        RPD()
    End Sub

    Private Sub BlankPage1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ActivateDisplay()
        AddHandler HardwareButtons.BackPressed, AddressOf BackPressed
        listBox.ItemsSource = LoadComboBoxData()
        AddHandler dt.Tick, AddressOf dispatcherTimer_Tick
        AddHandler dt_menu.Tick, AddressOf dispatcherTimer_MENU_Tick
        Me.InitializeComponent()
        dt.Interval = New TimeSpan(0, 0, 15)
        dt_conn.Interval = New TimeSpan(0, 0, 5)
        dt_menu.Interval = New TimeSpan(0, 0, 1)
        If StationValue Is Nothing Then
            localSettings.Values("STORED_ID") = "5"
        Else
            StoredStation = StationValue
        End If
        RPD()
        dt.Start()
    End Sub

    Private Sub btnPlayer_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles btnPlayer.Tapped
        If IS_PLAYING Then
            btnPlayer.Content = ""
            IS_PLAYING = False
            mediaElement.AutoPlay = False
            mediaElement.Source = New Uri(STREAM_URL)
            mediaElement.Stop()
        Else
            btnPlayer.Content = ""
            IS_PLAYING = True
            mediaElement.AutoPlay = True
            mediaElement.Source = New Uri(STREAM_URL)
            mediaElement.Play()
        End If
    End Sub

    Private Async Sub RPD()
        Dim value As Object = localSettings.Values("STORED_ID")
        Dim baseAddress = New Uri("http://ponyvillelive.com/api/")
        Dim subAddress As String = "nowplaying/index/id/" & strArrayIDStr(value).Replace(" ", "")
        Using httpClient = New Net.Http.HttpClient() With {.BaseAddress = baseAddress}
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache")
            Using response = Await httpClient.GetAsync(subAddress)
                Dim responseData As String = Await response.Content.ReadAsStringAsync()
                Dim nullify As String = ":""blank"""
                Dim nullstring As String = ":"""""
                Dim DisplayString As String
                DisplayString = responseData.ToString.Replace(nullstring, nullify)
                Try
                    Dim TestObject As Example = Global.Newtonsoft.Json.JsonConvert.DeserializeObject(Of Example)(DisplayString.ToString)
                    If TestObject.result.status = "online" Then 'In other words, if the station is online
                        CURRENT_TRACK = TestObject.result.current_song.title
                        If C_P_T <> CURRENT_TRACK Then
                            'Set STREAM_URL
                            STREAM_URL = TestObject.result.station.stream_url
                            'Set STATION_NAME
                            STATION_NAME = TestObject.result.station.name
                            'Set CURRENT_ARTIST
                            CURRENT_ARTIST = TestObject.result.current_song.artist
                            'Set station GENRE
                            GENRE = TestObject.result.station.genre
                            'Sets the RequestURL
                            RequestURL = TestObject.result.station.request_url
                            'Set STATION_URL
                            STATION_URL = TestObject.result.station.web_url
                            'Set CURRENT_LISTENERS
                            CURRENT_LISTENERS = TestObject.result.listeners.current
                            'Set CURRENT_IMAGE
                            CURRENT_IMAGE = TestObject.result.current_song.image_url
                            'Set CURRENT_STATION_LOGO
                            CURRENT_STATION_LOGO = TestObject.result.station.image_url
                            'Now, set txtArtist to CURRENT_ARTIST
                            'txtARTIST.Text = CURRENT_ARTIST
                            'Now, set txtTrackTitle to CURRENT_TRACK
                            'txtARTIST.Text = CURRENT_ARTIST & vbCrLf & CURRENT_TRACK
                            'If CURRENT_ARTIST.Length = 0 Then
                            'txtARTIST.Text = CURRENT_ARTIST & vbCrLf & CURRENT_TRACK
                            'Else
                            'txtARTIST.Text = CURRENT_TRACK
                            'End If
                            'txtLISTENERS_Copy.Text = CURRENT_ARTIST.Length
                            If RequestURL = "blank" Then
                                imgREQUEST.Visibility = Visibility.Collapsed
                            Else
                                imgREQUEST.Visibility = Visibility.Visible
                            End If
                            If CURRENT_ARTIST.Length > 0 Then
                                txtARTIST.Text = CURRENT_TRACK & " ~ ~ " & CURRENT_ARTIST
                            Else
                                txtARTIST.Text = CURRENT_TRACK
                            End If
                            If CURRENT_TRACK.Length > 0 Then
                                txtARTIST.Text = CURRENT_ARTIST & " ~ ~ " & CURRENT_TRACK
                            Else
                                txtARTIST.Text = CURRENT_ARTIST
                            End If
                            C_P_T = CURRENT_TRACK
                            For a = 0 To 4
                                ARTIST_HISTORY(a) = TestObject.result.song_history.ElementAt(a).song.artist
                                TRACK_HISTORY(a) = TestObject.result.song_history.ElementAt(a).song.title
                            Next
                            'Set Recently_Played to the last 5 played tracks
                            Recently_Played = "♫ 1:" & vbCrLf & ARTIST_HISTORY(0) & vbCrLf & TRACK_HISTORY(0) & vbCrLf & "♫ 2:" & vbCrLf & ARTIST_HISTORY(1) & vbCrLf & TRACK_HISTORY(1) & vbCrLf & "♫ 3:" & vbCrLf & ARTIST_HISTORY(2) & vbCrLf & TRACK_HISTORY(2) & vbCrLf & "♫ 4:" & vbCrLf & ARTIST_HISTORY(3) & vbCrLf & TRACK_HISTORY(3) & vbCrLf & "♫ 5:" & vbCrLf & ARTIST_HISTORY(4) & vbCrLf & TRACK_HISTORY(4)
                            'Check to see if the current station has a request feature
                            'EnableRequest()
                            LOGO = New Uri(CURRENT_STATION_LOGO)
                            imgSTATION.Source = New BitmapImage(LOGO)
                        End If
                        'Set txtListeners to CURRENT_LISTENERS
                        If CURRENT_LISTENERS < 10 Then
                            txtLISTENERS.Text = "0" & CURRENT_LISTENERS
                        Else
                            txtLISTENERS.Text = CURRENT_LISTENERS
                        End If
                        If NEW_STATION Then
                            If IS_PLAYING Then
                                mediaElement.Source = New Uri(STREAM_URL)
                                mediaElement.Play()
                                NEW_STATION = False
                            Else
                                mediaElement.Stop()
                                NEW_STATION = False
                            End If
                        End If
                        btnPlayer.IsEnabled = True
                        ST_OFFLINE = False
                    Else
                        'OffLine()
                        ST_OFFLINE = True
                    End If
                Catch
                End Try
            End Using
        End Using

    End Sub

    Private Sub listBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles listBox.SelectionChanged
        Dim selectedindex As Integer
        Dim value As Object = localSettings.Values("STORED_ID")
        selectedindex = listBox.SelectedIndex
        localSettings.Values("STORED_ID") = selectedindex
        NEW_STATION = True
        RPD()
    End Sub

    Sub DisplayInfo(ByVal title As String, ByVal info As String)
        MyScrollViewer.ChangeView(Nothing, 0, Nothing, True)

        gridINFO.Visibility = Visibility.Visible
        txtInfoTitle.Text = title
        txtInfoInfo.Text = info
    End Sub

    Private Sub btnRADIO_Click(sender As Object, e As RoutedEventArgs) Handles btnRADIO.Click
        btnRADIO.BorderThickness = New Thickness(2, 2, 2, 2)
        btnHISTORY.BorderThickness = New Thickness(0, 0, 0, 0)
        btnABOUT.BorderThickness = New Thickness(0, 0, 0, 0)
    End Sub

    Private Sub btnHISTORY_Click(sender As Object, e As RoutedEventArgs) Handles btnHISTORY.Click
        btnRADIO.BorderThickness = New Thickness(0, 0, 0, 0)
        btnHISTORY.BorderThickness = New Thickness(2, 2, 2, 2)
        btnABOUT.BorderThickness = New Thickness(0, 0, 0, 0)
        DisplayInfo("Recently Played", "Station Name:" & vbCrLf & STATION_NAME & vbCrLf & vbCrLf & "Genre:" & vbCrLf & GENRE & vbCrLf & vbCrLf & Recently_Played)
    End Sub

    Private Sub btnABOUT_Click(sender As Object, e As RoutedEventArgs) Handles btnABOUT.Click
        btnRADIO.BorderThickness = New Thickness(0, 0, 0, 0)
        btnHISTORY.BorderThickness = New Thickness(0, 0, 0, 0)
        btnABOUT.BorderThickness = New Thickness(2, 2, 2, 2)
        DisplayInfo("About All Pony Radio App", "This Is a third-party mobile application For accessing the Ponyville Live! radio streams." & vbCrLf & vbCrLf & "It gives you access To all Of Ponyville Lives' radio stations, playing some of the best brony music, and pumped straight to your ears!" & vbCrLf & vbCrLf & "This app was lovingly created using Visual Studio 2015 Community.")
    End Sub

    Private Sub btnInfoClose_Click(sender As Object, e As RoutedEventArgs) Handles btnInfoClose.Click
        btnRADIO.BorderThickness = New Thickness(2, 2, 2, 2)
        btnHISTORY.BorderThickness = New Thickness(0, 0, 0, 0)
        btnABOUT.BorderThickness = New Thickness(0, 0, 0, 0)
        gridINFO.Visibility = Visibility.Collapsed
    End Sub

    Private Sub imgREQUEST_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles imgREQUEST.Tapped
        OpenInBrowser(RequestURL)
    End Sub

    Private Async Sub OpenInBrowser(ByVal k As String)

        Dim uri As New Uri(k)

        ' Launch the URI
        Dim success = Await Windows.System.Launcher.LaunchUriAsync(uri)

        If success Then
            ' URI launched
        Else
            ' URI launch failed
        End If
    End Sub

End Class
