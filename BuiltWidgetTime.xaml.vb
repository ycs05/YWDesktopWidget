Imports System.Windows.Threading

Class BuiltWidgetTime
    Private Sub Rectangle_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If e.ButtonState = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub
    Private ReadOnly _timer As New DispatcherTimer()
    Public Sub New()
        InitializeComponent()
        _timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler _timer.Tick, AddressOf UpdateNowTime
        _timer.Start()
    End Sub
    Private Sub UpdateNowTime(sender As Object, e As EventArgs)
        Time.Content = DateTime.Now.ToString("HH:mm")
    End Sub
End Class