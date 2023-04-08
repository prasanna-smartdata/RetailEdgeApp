Class KPIReport
    Public Sub New(ByRef viewModel As SuperstoreProcessViewModel)

        ' This call is required by the designer.
        InitializeComponent()
        Me.DataContext = viewModel
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
