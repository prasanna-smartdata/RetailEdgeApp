Class MainWindow

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'tbPages.Items(0) = New ClientMaintenancePage()
        'tbPages.Items(1) = New ManageSuperstores()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
