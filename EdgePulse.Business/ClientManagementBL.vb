Imports System.Data
Imports System.Data.SqlClient
Imports EdgePulse.Data
Imports EdgePulse.Entities
Imports EdgePulse.Infrastructure

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

    Function UpdateClient(ByVal Client As ClientEntity) As Boolean


        Try
            Dim clientDL As New ClientDL()
            Return clientDL.UpdateClient(Client)

        Catch ex As Exception

        End Try


        Return True



    End Function

#End Region


#Region "Add Edit Client-Store-Superstore"

#End Region


End Class
