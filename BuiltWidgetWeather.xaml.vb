Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Windows

Public Class BuiltWidgetWeather
    Public Sub New()
        InitializeComponent()
        AddHandler Loaded, AddressOf BuiltWidgetWeather_Loaded
        AddHandler Unloaded, AddressOf Widget_Unloaded
    End Sub

    Private Async Sub BuiltWidgetWeather_Loaded(sender As Object, e As RoutedEventArgs)
        RefreshButton_Click(Nothing, Nothing)
    End Sub

    Private Sub Rectangle_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If e.ButtonState = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

    Private Class WeatherResponse
        Public Property province As String
        Public Property city As String
        Public Property district As String
        Public Property adcode As String
        Public Property weather As String
        Public Property weather_icon As String
        Public Property temperature As Integer
        Public Property wind_direction As String
        Public Property wind_power As String
        Public Property humidity As Integer
        Public Property report_time As String
    End Class

    Private ReadOnly httpClient As New HttpClient()

    Private Async Sub RefreshButton_Click(sender As Object, e As RoutedEventArgs)
        Dim btn = TryCast(sender, System.Windows.Controls.Button)
        If btn IsNot Nothing Then btn.IsEnabled = False
        Try
            Dim apiUrl As String = "https://uapis.cn/api/v1/misc/weather"
            Dim jsonResponse As String = Await httpClient.GetStringAsync(apiUrl)
            Dim weatherData = JsonConvert.DeserializeObject(Of WeatherResponse)(jsonResponse)
            LocationTextBlock.Content = $"{weatherData.province}，{weatherData.city}，{weatherData.district}"
            WeatherTextBlock.FontSize = 48
            WeatherTextBlock.Content = $"{weatherData.temperature}℃"
            ' WindTextBlock.Content = $"{weatherData.wind_direction} {weatherData.wind_power}，湿度：{weatherData.humidity}%"
            ' UpdateTimeTextBlock.Content = $"数据发布时间：{weatherData.report_time}"
        Catch ex As HttpRequestException
            MessageBox.Show($"网络请求失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            WeatherTextBlock.FontSize = 16
        Catch ex As JsonException
            MessageBox.Show($"数据解析失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            WeatherTextBlock.FontSize = 16
        Catch ex As Exception
            MessageBox.Show($"发生未知错误：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            WeatherTextBlock.FontSize = 16
        Finally
            If btn IsNot Nothing Then btn.IsEnabled = True
        End Try
    End Sub

    Private Sub Close_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub Widget_Unloaded(sender As Object, e As RoutedEventArgs)
        RemoveHandler Loaded, AddressOf BuiltWidgetWeather_Loaded
        RemoveHandler Unloaded, AddressOf Widget_Unloaded
        httpClient.Dispose()
    End Sub
End Class