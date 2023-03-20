Class MainWindow
    Private Sub selectClients_Loaded(sender As Object, e As RoutedEventArgs) Handles selectClients.Loaded
        Dim superStores As New List(Of String) From {"NZ Showcase", "Showcase", "OZNZ All", "Edge USA - A3", "Nationalwide", "Edge USA"}

        Me.superStores.ItemsSource = superStores





        Dim clientNames = New List(Of Client) From
    {
        New Client("1001", "Demo - USA"),
        New Client("1002", "The Edge - Arnett Jevelery"),
        New Client("1003", "The Edge - Glasscock"),
          New Client("1004", "The Edge - Elisa llana"),
        New Client("1005", "The Edge - Arnett Jevelery"),
        New Client("1006", "The Edge - Molinelli"),
          New Client("1007", "The Edge - Martin"),
        New Client("1008", "The Edge - Sather Jevelery"),
        New Client("1009", "The Edge - Mithum")
    }
        Me.selectClients.ItemsSource = clientNames

    End Sub
End Class

Public Class Client
    Public Property clientNumber As String
    Public Property clientName As String

    Public Sub New(ByVal number As String, ByVal name As String)
        clientNumber = number
        clientName = name
    End Sub

End Class
