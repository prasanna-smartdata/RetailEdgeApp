Class MainWindow
    Private Sub mainWin_Loaded(sender As Object, e As RoutedEventArgs) Handles mainWin.Loaded
        mainWin.Content = New CleintMaintenance()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
