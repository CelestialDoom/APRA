' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

Imports System.Net.Http
Imports Windows.Phone.UI.Input
Imports Windows.UI
''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Public dt As DispatcherTimer = New DispatcherTimer()
    Public dt_conn As DispatcherTimer = New DispatcherTimer()
    Public Loading_Var As Integer = 0
    Public localSettings As Windows.Storage.ApplicationDataContainer = Windows.Storage.ApplicationData.Current.LocalSettings
    Public MenuOpen As Boolean = False
    Public ShowStationNames As String

    Public sResult As String = ""

    Public StoredStation As String
    Public StoredTheme As String
    Public TimerCount As Integer = 0
    Sub BackPressed(sender As Object, e As BackPressedEventArgs)

        e.Handled = True

    End Sub

    Sub CreateTimer(days As Integer, hours As Integer,
            minutes As Integer, seconds As Integer, millisec As Integer)

        Dim elapsedTime As New TimeSpan(days, hours, minutes, seconds, millisec)

    End Sub

    Public Sub DisplayPlatform()

        Dim platformFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily

        If platformFamily = "Windows.Desktop" Then
            Is_Desktop = True
        Else
            Is_Desktop = False
        End If
    End Sub

    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.
    ''' This parameter is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
        ' TODO: Prepare the page for display here.

        ' TODO: If your application contains multiple pages, ensure that you are
        ' handling the hardware Back button by registering for the
        ' Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
        ' If you are using the NavigationHelper provided by some templates,
        ' this event is handled for you.
    End Sub
    Private Async Sub GetStations()

        Dim baseAddress = New Uri("http://ponyvillelive.com/api/")
        Dim subAddress As String = "station/list/category/audio"
        Using httpClient = New HttpClient() With {.BaseAddress = baseAddress}
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache")
            Using response = Await httpClient.GetAsync(subAddress)
                Dim responseData As String = Await response.Content.ReadAsStringAsync()
                Dim nullify As String = ":""blank"""
                Dim nullstring As String = ":"""""
                Dim DisplayString As String
                DisplayString = responseData.ToString.Replace(nullstring, nullify)
                Try
                    Dim TestObject As StationList = Global.Newtonsoft.Json.JsonConvert.DeserializeObject(Of StationList)(DisplayString.ToString)
                    If TestObject.status = "success" Then 'In other words, if the station is online
                        TotalStations = TestObject.result.Count
                        ReDim strArray(TotalStations - 1)
                        ReDim strArrayIDStr(TotalStations - 1)
                        ReDim strArrayID(TotalStations - 1)
                        ReDim strArrayLogo(TotalStations - 1)
                        For a = 0 To TotalStations - 1
                            strArray(a) = TestObject.result.ElementAt(a).name
                            strArrayIDStr(a) = TestObject.result.ElementAt(a).id.ToString
                            strArrayID(a) = TestObject.result.ElementAt(a).id
                            strArrayLogo(a) = TestObject.result.ElementAt(a).image_url
                            ShowStationNames = String.Join(vbCrLf, strArray)
                        Next
                        Me.Frame.Navigate(GetType(BlankPage1))
                    Else
                        Application.Current.Exit()
                    End If
                Catch
                End Try
            End Using
        End Using

    End Sub

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Dim ThemeValue As Object = localSettings.Values("STORED_THEME")
        Dim StationValue As Object = localSettings.Values("STORED_ID")
        Dim value As Object = localSettings.Values("STORED_ID")
        Me.InitializeComponent()
        Me.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled
        DisplayPlatform()
        If Not Is_Desktop Then
            AddHandler HardwareButtons.BackPressed, AddressOf BackPressed
        End If
        Dim bounds = ApplicationView.GetForCurrentView().VisibleBounds
        Dim scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel
        Dim size = New Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor)
        If Not Is_Desktop Then
            If size.Height > size.Width Then
                gridPORT.Visibility = Visibility.Visible
                gridHORIZ.Visibility = Visibility.Collapsed
            Else
                gridPORT.Visibility = Visibility.Collapsed
                gridHORIZ.Visibility = Visibility.Visible
            End If
        Else
            gridPORT.Visibility = Visibility.Visible
            gridHORIZ.Visibility = Visibility.Collapsed
        End If

        If ThemeValue Is Nothing Then
            localSettings.Values("STORED_THEME") = "6"
        Else
            StoredTheme = ThemeValue
        End If
        GetStations()

    End Sub

    Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged

        Dim bounds = ApplicationView.GetForCurrentView().VisibleBounds
        Dim scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel
        Dim size = New Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor)
        If Not Is_Desktop Then
            If size.Height > size.Width Then
                gridPORT.Visibility = Visibility.Visible
                gridHORIZ.Visibility = Visibility.Collapsed
            Else
                gridPORT.Visibility = Visibility.Collapsed
                gridHORIZ.Visibility = Visibility.Visible
            End If
        Else
            Dim MinWidth As Integer = 720
            Dim MinHeight As Integer = 615
            If Window.Current.Bounds.Height < MinHeight Then
                ApplicationView.GetForCurrentView().TryResizeView(New Size() With {.Width = Window.Current.Bounds.Width, .Height = MinHeight})
            End If
            If Window.Current.Bounds.Width < MinWidth Then
                ApplicationView.GetForCurrentView().TryResizeView(New Size() With {
                    .Width = MinWidth,
                    .Height = Window.Current.Bounds.Height
                })
            End If
            If Window.Current.Bounds.Width < (Window.Current.Bounds.Height / MinHeight * MinWidth) Then
                ApplicationView.GetForCurrentView().TryResizeView(New Size() With {
                    .Width = Window.Current.Bounds.Height / MinHeight * MinWidth,
                    .Height = Window.Current.Bounds.Height
                })
            End If
        End If
    End Sub

End Class
