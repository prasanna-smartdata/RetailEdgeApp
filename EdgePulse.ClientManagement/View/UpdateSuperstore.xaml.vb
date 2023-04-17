Public Class UpdateSuperstore
    Public Sub New(ByRef ViewModel As ManageSuperstoreViewModel)

        ' This call is required by the designer.
        InitializeComponent()
        Me.DataContext = ViewModel
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
