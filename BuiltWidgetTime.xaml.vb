Imports System.Windows.Threading

Class BuiltWidgetTime
    Private ReadOnly _timer As New DispatcherTimer()

    Public Sub New()
        InitializeComponent()
        _timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler _timer.Tick, AddressOf UpdateNowTime
        _timer.Start()
        AddHandler Me.Unloaded, AddressOf Widget_Unloaded
    End Sub

    Private Sub Rectangle_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If e.ButtonState = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

    Private Sub UpdateNowTime(sender As Object, e As EventArgs)
        Time.Content = DateTime.Now.ToString("HH:mm")
    End Sub

    Private Sub Cloce_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub Widget_Unloaded(sender As Object, e As RoutedEventArgs)
        _timer.Stop()
        RemoveHandler _timer.Tick, AddressOf UpdateNowTime
        RemoveHandler Me.Unloaded, AddressOf Widget_Unloaded
    End Sub
End Class