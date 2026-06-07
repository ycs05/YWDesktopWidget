Imports System.Drawing
Imports System.Windows.Forms

Class MainWindow
    Private trayIcon As New NotifyIcon()
    Private trayMenu As New ContextMenuStrip()
    Private Sub Rectangle_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If e.ButtonState = MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
        trayMenu.Items.Add("显示窗口", Nothing, AddressOf ShowWin)
        trayMenu.Items.Add("退出程序", Nothing, AddressOf ExitProg)
        trayIcon.Icon = New Icon("Logo.ico")
        trayIcon.Text = "YW Desktop Widget"
        trayIcon.ContextMenuStrip = trayMenu
        AddHandler trayIcon.MouseDoubleClick, AddressOf DoubleClickTray
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.ShowInTaskbar = False
        Me.Visibility = Visibility.Hidden
        trayIcon.Visible = True
    End Sub

    Private Sub ShowWin(sender As Object, e As EventArgs)
        Me.ShowInTaskbar = True
        Me.Visibility = Visibility.Visible
        Me.WindowState = WindowState.Normal
    End Sub

    Private Sub DoubleClickTray(sender As Object, e As MouseEventArgs)
        Call ShowWin(Nothing, Nothing)
    End Sub

    Private Sub ExitProg(sender As Object, e As EventArgs)
        trayIcon.Visible = False
        Application.Current.Shutdown()
    End Sub
    Private Sub ShowTimeWidget(sender As Object, e As RoutedEventArgs)
        Dim timewidget As New BuiltWidgetTime()
        timewidget.Show()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As RoutedEventArgs)
        Application.Current.Shutdown()
    End Sub
End Class