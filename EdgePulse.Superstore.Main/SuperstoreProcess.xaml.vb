Class SuperstoreProcess

    Dim viewModel As New SuperstoreProcessViewModel()
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        AddHandler viewModel.BuildSuperstoreCompletedEvent, AddressOf LoadKPIReports
        AddHandler viewModel.RestartSuperstoreProcessEvent, AddressOf LoadBuildProcess

        Me.DataContext = viewModel
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub LoadKPIReports()
        Try
            ' mainWin.Navigate(New KPIReport(viewModel))

            mainWin.Content = New KPIReport(viewModel)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LoadBuildProcess()
        Try

            mainWin.Content = New ProcessForm(viewModel)
            viewModel.LoadSuperstores()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub StackPanel_Loaded(sender As Object, e As RoutedEventArgs)
        LoadBuildProcess()
    End Sub
End Class
