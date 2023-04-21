Imports EdgePulse.Entities

Public Class UpdateSuperstore
    Public Sub New(ByRef ViewModel As ManageSuperstoreViewModel)

        ' This call is required by the designer.
        InitializeComponent()
        If ViewModel.SelectedSuperstore Is Nothing Then
            ViewModel.SelectedSuperstore = New SuperstoreEntity
        End If
        Me.DataContext = ViewModel
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class
