Imports EdgePulse.Data
Imports EdgePulse.Entities

Public Class ClientManagementBL


#Region "Add Edit Client"

    Function GetClients() As List(Of ClientEntity)

        Try
            Dim clientDL As New ClientDL()
            Return clientDL.GetClients()
        Catch ex As Exception

        End Try

        Return Nothing
    End Function

#End Region


#Region "Add Edit Client-Store-Superstore"

#End Region


End Class
