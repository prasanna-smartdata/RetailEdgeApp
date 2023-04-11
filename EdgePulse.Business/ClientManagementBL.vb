Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

Public Class ClientManagementBL


#Region "Add Edit Client"

    Shared Function GetClients() As List(Of ClientEntity)

        Try
            Dim clientDL As New ClientDL()
            Return clientDL.GetClients()
        Catch ex As Exception
            Throw
        End Try

        Return Nothing
    End Function

    Shared Function GetClientDetails(ByVal clientId As Int32) As ClientEntity

        Try
            Dim clientDL As New ClientDL()
            Return clientDL.GetClientDetails(clientId)
        Catch ex As Exception
            Throw
        End Try
        Return Nothing
    End Function
    Function UpdateClient(ByVal Client As ClientEntity) As Boolean


        Try
            Dim clientDL As New ClientDL()
            Return clientDL.UpdateClient(Client)

        Catch ex As Exception
            Throw
        End Try


        Return True



    End Function

#End Region


#Region "Add Edit Client-Store-Superstore"

#End Region


End Class
