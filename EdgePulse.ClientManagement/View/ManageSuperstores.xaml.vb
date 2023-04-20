Public Class ManageSuperstores

    Dim viewModel As New ManageSuperstoreViewModel()
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.DataContext = viewModel
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub btnEditSuperstore_Click(sender As Object, e As RoutedEventArgs)
        Try

            Dim superStoreUpdate As New UpdateSuperstore(viewModel)
            superStoreUpdate.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub


End Class
